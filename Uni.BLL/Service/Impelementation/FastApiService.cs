using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Uni.BLL.Service.Abstraction;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Extensions.Http;
using IHttpClientFactory = System.Net.Http.IHttpClientFactory;
namespace Uni.BLL.Service.Implementation
{
	public class FastApiService : IFastApiService
	{
		private readonly HttpClient _httpClient;

		public FastApiService(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("FastAPI");
			_httpClient.BaseAddress = new Uri("http://localhost:8000");
		}

		public async Task<string> UploadStringAsync(string prompt)
		{
			var requestBody = new Dictionary<string, string>
	{
		{ "user_input", prompt }
	};

			var jsonContent = new StringContent(
				JsonConvert.SerializeObject(requestBody),
				Encoding.UTF8,
				"application/json"
			);

			var response = await _httpClient.PostAsync("/chat/", jsonContent);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsStringAsync();
		}

	}
}
