import time 
import os
import json
import re

from fastapi import FastAPI, HTTPException
from dotenv import load_dotenv
from pydantic import BaseModel
from openai import OpenAI, APIError, APIConnectionError
from functools import wraps


app = FastAPI()
load_dotenv()
CLIENT = None
INT_SYS_PROMPT, GEN_SYS_PROMPT, DATA_SYS_PROMPT = None, None, None
MODEL = "microsoft/mai-ds-r1:free"


class UserPrompt(BaseModel):
    user_input: str


class FormatRequest(BaseModel):
    original_query: str
    db_results: str


def get_client():
    """Initialize the OpenAI client with the API key and base URL."""
    api_key = os.getenv("DEEPSEEK_API_KEY")
    if not api_key:
        raise ValueError("""
        Missing API key! Please:
        1. Create .env file
        2. Add DEEPSEEK_API_KEY=your_key
        """)
    
    return OpenAI(api_key=api_key,
        base_url=os.getenv("BASE_URL", "https://api.deepseek.com/v1")
    )


def handle_deepseek_errors(func): # `func` is the function to be decorated (e.g., `chat_endpoint`)
    """Decorator to handle DeepSeek API errors and retry logic."""
    @wraps(func)
    def wrapper(*args, **kwargs): # `args` and `kwargs` are the arguments passed to the function `func`
        """Wrapper function to handle errors and retries."""
        max_retries = 3
        retry_delay = 2

        for attempt in range(max_retries):
            try:
                return func(*args, **kwargs) # Call the original function `func`, in this case chat_endpoint, with its arguments
            except APIError as e:
                error_data = e.response.json() if e.response else {}
                error_message = error_data.get('error', {}).get('message', 'Unknown error')
                error_code = e.status_code
                
                if error_code == 401: 
                    print("401 Error: Authentication Failed.\nVerify your API key.")
                    return "Authentication Error - Check API credentials"
                
                elif error_code == 402:
                    print("402 Error: Insufficient Balance.\nAdd funds to your account.")
                    return "Account Balance Depleted - Please top up"
                
                elif error_code == 422:
                    print("422 Error: Invalid Parameters.\nCheck token parameters.")
                    return "Invalid parameters provided"
                
                elif error_code == 429:
                    wait_time = 2 ** (attempt + 1)
                    print(f"429 Error: Rate Limit Exceeded. Retrying in {wait_time}s...")
                    time.sleep(wait_time)
                    continue

                elif error_code == 500:
                    print("500 Error: Internal Server Error. Retrying...")
                    if attempt < max_retries - 1:
                        time.sleep(retry_delay)
                        continue
                    return "Internal Server Error - Please try again later"
                
                elif error_code == 503:
                    print("503 Error: Serever is overloaded. Retrying...")
                    if attempt < max_retries - 1:
                        time.sleep(retry_delay)
                        continue
                    print("Server is overloaded, switch to backup provider.")
                    return "Server Overloaded - Please try again later"
                
                else:
                    print(f"Unexpected Error {error_code}: {error_message}")
                    return "Unexpected error occurred"
                
            except APIConnectionError  as e:
                print(f"Connection Error: {str(e)}")
                if attempt < max_retries - 1:
                    print(f"Retrying in {retry_delay}s...")
                    time.sleep(retry_delay)
                    continue
                return "Network connection failed"
            
        return func(*args, **kwargs)
    return wrapper


@app.on_event("startup")
async def startup_event():
    """Load environment variables and initialize the DeepSeek client."""
    global CLIENT, INT_SYS_PROMPT, GEN_SYS_PROMPT, DATA_SYS_PROMPT
    CLIENT = get_client()
    INT_SYS_PROMPT, GEN_SYS_PROMPT, DATA_SYS_PROMPT = load_context()


@app.post("/chat")
@handle_deepseek_errors
def chat_endpoint(request: UserPrompt):
    """Chatbot endpoint to handle user queries."""
    user_input = re.escape(request.user_input)
    if analyze_intent(user_input):
        sql_query = generate_sql(user_input)
        return {"action": "execute_sql", "query": sql_query}
    else:
        return generate_response(user_input)
    

@app.post("/format-response")
@handle_deepseek_errors
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


def analyze_intent(user_input: str) -> dict:
    """Analyze the user's intent based on the input."""
    response = CLIENT.chat.completions.create(
        model=MODEL,
        messages=[{"role": "system", "content": INT_SYS_PROMPT}, 
                  {"role": "user", "content": user_input}], 
        temperature=0.1,
    )

    requires_data = response.choices[0].message.content.strip().lower()
    try:
        if requires_data not in ["true", "false"]:
            raise ValueError("Invalid response format")
        return requires_data == "true"
    except (json.JSONDecodeError, ValueError) as e:
        raise HTTPException(status_code=400, detail=f"Invalid response format from intent analysis. Output:\n{response.choices[0].message.content}")


def generate_response(user_input: str) -> str:
    response = CLIENT.chat.completions.create(
        model=MODEL,
        messages=[{"role": "system", "content": GEN_SYS_PROMPT}, 
                  {"role": "user", "content": user_input}], 
        temperature=0.3, top_p=0.6, 
        frequency_penalty=0.4, presence_penalty=0.2,
        max_tokens=400,
    )
    return {"response": response.choices[0].message.content}


def generate_sql(user_input: str) -> str:
    response = CLIENT.chat.completions.create(
        model="deepseek/deepseek-chat-v3-0324:free",
        messages=[{"role": "system", "content": DATA_SYS_PROMPT}, 
                  {"role": "user", "content": user_input}], 
        temperature=0.0, max_tokens=100,
    )
    sql_query = (response.choices[0].message.content)
    if not validate_sql(sql_query):
        raise HTTPException(status_code=301, detail="Invalid SQL query generated. Output:\n" + sql_query)
    return sql_query


def validate_sql(sql_query: str) -> bool:
    query_lower = sql_query.lower().strip()
    forbidden_keywords = {"insert", "update", "delete", "drop", "alter", "create", "truncate"}

    if any(keyword in query_lower for keyword in forbidden_keywords):
        return False
    
    if not re.match(r"^\s*select", query_lower, re.IGNORECASE):
        return False
    
    pattern = re.compile(r"\b\w+\s*=\s*@user_id\b", re.IGNORECASE)
    return bool(pattern.search(query_lower))


def load_context():
    """Load system prompts from the context file."""
    with open("context.txt", 'r', encoding='utf-8') as f:
        content = f.read()
    parts = re.split(r'### (INTENT|GENERAL|SQL) SYSTEM PROMPT ###', content)
    
    intent_section = parts[2].strip() if len(parts) > 1 else ""
    general_section = parts[4].strip() if len(parts) > 3 else ""
    sql_section = parts[6].strip() if len(parts) > 5 else ""
    return intent_section, general_section, sql_section


if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)