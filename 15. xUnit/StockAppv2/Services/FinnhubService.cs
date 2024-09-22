using Microsoft.Extensions.Configuration;
using ServiceContracts;
using Models;
using System.Text.Json;
using System.Net.Http;

namespace Services
{
    public class FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IConfiguration _configuration = configuration;
        public async Task<CompanyProfile> GetCompanyProfile(string? symbol)
        {
            string url = $"https://finnhub.io/api/v1/stock/profile2?symbol={symbol}&token={_configuration["Token"]}";
            return await SendRequest<CompanyProfile>(url);
        }
        public async Task<StockPriceQuote> GetStockPriceQuote(string? symbol)
        {
            string url = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration["Token"]}";
            return await SendRequest<StockPriceQuote>(url);
        }

        private async Task<T> SendRequest<T>(string url) where T : new()
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new()
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                T? response = JsonSerializer.Deserialize<T>(responseBody, options);

                return response ?? throw new InvalidOperationException("No response from Finnhub service.");
            }
        }
    }
}
