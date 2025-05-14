import asyncio 
import os
import json
import re

from fastapi import FastAPI, HTTPException
from dotenv import load_dotenv
from pydantic import BaseModel
from openai import AsyncOpenAI, APIError, APIConnectionError
from functools import wraps


app = FastAPI()
load_dotenv()
CLIENT = None
INT_SYS_PROMPT, GEN_SYS_PROMPT, DATA_SYS_PROMPT = None, None, None
MODEL = "deepseek/deepseek-chat-v3-0324:free"


class UserPrompt(BaseModel):
    user_input: str


class FormatRequest(BaseModel):
    original_query: str
    db_results: str


def get_client():
    """Initialize the OpenAI client with the API key and base URL."""
    api_key = os.getenv("CHATBOT_API_KEY")
    if not api_key:
        raise ValueError("""
        Missing API key! Please:
        1. Create .env file
        2. Add CHATBOT_API_KEY=your_key
        """)
    
    return AsyncOpenAI(api_key=api_key,
                       base_url=os.getenv("BASE_URL", "https://api.deepseek.com/v1")
    )


def handle_llm_errors(func):
    """Universal error handler for LLM APIs (OpenRouter/DeepSeek)"""
    @wraps(func)
    async def wrapper(*args, **kwargs):
        max_retries = 3
        retry_delay = 2
        error_messages = {
            401: "Authentication failed - check API key",
            402: "Insufficient credits/balance - please top up",
            403: "Model access denied",
            400: "Model not found",
            422: "Invalid request parameters",
            429: "Rate limit exceeded",
            500: "Internal server error",
            503: "Service unavailable"
        }

        for attempt in range(max_retries):
            try:
                return await func(*args, **kwargs)
            
            except APIError as e:
                # Unified error response parsing
                error_data = getattr(e, "response", {}).json().get("error", {})
                error_code = error_data.get("code", e.status_code)
                error_msg = error_data.get("message", "")
                provider = "OpenRouter" if "openrouter" in str(e).lower() else "DeepSeek"

                print(f"{provider} Error {error_code}: {error_msg}")
                
                # Handle retryable status codes
                if error_code in [429, 500, 503]:
                    delay = int(e.response.headers.get("Retry-After", retry_delay))
                    print(f"Retrying in {delay}s (Attempt {attempt+1}/{max_retries})")
                    await asyncio.sleep(delay)
                    continue
                
                # Return user-friendly messages
                return {"error": error_messages.get(error_code, "Unknown error occurred")}

            except APIConnectionError as e:
                print(f"Network error: {str(e)}")
                if attempt < max_retries - 1:
                    await asyncio.sleep(retry_delay)
                    continue
                return {"error": "Connection failed after retries"}

        return {"error": "Maximum retries exceeded"}

    return wrapper


@app.on_event("startup")
async def startup_event():
    """Load environment variables and initialize the DeepSeek client."""
    global CLIENT, INT_SYS_PROMPT, GEN_SYS_PROMPT, DATA_SYS_PROMPT
    CLIENT = get_client()
    INT_SYS_PROMPT, GEN_SYS_PROMPT, DATA_SYS_PROMPT = load_context()


@app.post("/chat")
@handle_llm_errors
async def chat_endpoint(request: UserPrompt):
    """Chatbot endpoint to handle user queries."""
    if await analyze_intent(request.user_input):
        sql_query = await generate_sql(request.user_input)
        if not validate_sql(sql_query):
            raise HTTPException(status_code=301, detail="Invalid SQL query generated. Output:\n" + sql_query)
        return {"query": sql_query}
    else:
        return await generate_response(request.user_input)
    

@app.post("/format-response")
@handle_llm_errors
def format_results(request: FormatRequest):
    """Format the SQL query results into a user-friendly response."""
    formatting_prompt = f"""
    Original SQL query: {request.original_query}
    Database result: {json.dumps(request.db_results)}

    Format as markdown table with:
    - Student-friendly emojis
    - Hidden technical details
    - Clear section headings 
    """
    response = CLIENT.chat.completions.create(
        model=MODEL,
        messages=[{"role": "system", "content": GEN_SYS_PROMPT}, 
                  {"role": "user", "content": formatting_prompt}],
        temperature=0.2,
    )
    return {"formatted_response": response.choices[0].message.content}


async def analyze_intent(user_input: str) -> bool:
    """Analyze the user's intent based on the input."""
    response = await CLIENT.chat.completions.create(
        model=MODEL,
        messages=[{"role": "system", "content": INT_SYS_PROMPT}, 
                  {"role": "user", "content": user_input}], 
        temperature=0.0,
    )

    requires_data = response.choices[0].message.content.strip().lower()
    try:
        if requires_data not in ["true", "false"]:
            raise ValueError("Invalid response format")
        return requires_data == "true"
    except (json.JSONDecodeError, ValueError) as e:
        raise HTTPException(status_code=400, 
                            detail=f"Invalid response format from intent analysis. Output:\n{response.choices[0].message.content}")


async def generate_response(user_input: str) -> str:
    response = await CLIENT.chat.completions.create(
        model=MODEL,
        messages=[{"role": "system", "content": GEN_SYS_PROMPT}, 
                  {"role": "user", "content": user_input}], 
        temperature=0.3, top_p=0.6, 
        frequency_penalty=0.4, presence_penalty=0.2,
        max_tokens=400,
    )
    return {"response": response.choices[0].message.content}


async def generate_sql(user_input: str) -> str:
    response = await CLIENT.chat.completions.create(
        model=MODEL,
        messages=[{"role": "system", "content": DATA_SYS_PROMPT}, 
                  {"role": "user", "content": user_input}], 
        temperature=0.2, max_tokens=200,
    )
    return response.choices[0].message.content


def validate_sql(sql_query: str) -> bool:
    query_lower = sql_query.lower().strip()
    forbidden_keywords = {"insert", "update", "delete", "drop", "alter", "create", "truncate"}

    if any(keyword in query_lower for keyword in forbidden_keywords):
        return False
    
    if not re.match(r"^\s*select", query_lower, re.IGNORECASE):
        return False
    
    pattern = re.compile(r"@user_id\b", re.IGNORECASE)
    return bool(pattern.search(query_lower))


def load_context():
    """Load system prompts from the context file."""
    with open("context.txt", 'r', encoding='utf-8') as f:
        content = f.read()
    parts = re.split(r'# (INTENT|GENERAL|SQL) SYSTEM PROMPT #', content)
    
    intent_section = parts[2].strip() if len(parts) > 1 else ""
    general_section = parts[4].strip() if len(parts) > 3 else ""
    sql_section = parts[6].strip() if len(parts) > 5 else ""
    return intent_section, general_section, sql_section


if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)