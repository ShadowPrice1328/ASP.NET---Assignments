using System.Text.Json;
using Microsoft.Extensions.Options;
using StockApp.ServiceContracts;

namespace StockApp.Services;

public class FinnhubService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : IFinnhubService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;
    public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
    {
        string? token = _configuration.GetValue<string>("Token");
        string uri = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={token}";
        
        return await GetResponse(uri);
    }

    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
    {
        string? token = _configuration.GetValue<string>("Token");
        string uri = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}";

        return await GetResponse(uri);
    }
    
    private async Task<Dictionary<string, object>?> GetResponse(string uri)
    {
        using (HttpClient httpClient = _httpClientFactory.CreateClient())
        {
            HttpRequestMessage httpRequestMessage = new()
            {
                RequestUri = new Uri(uri),
                Method = HttpMethod.Get
            };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            Stream stream = httpResponseMessage.Content.ReadAsStream();
            StreamReader streamReader = new(stream);

            string response = streamReader.ReadToEnd();

            Dictionary<string, object>? pairs = JsonSerializer.Deserialize<Dictionary<string, object>?>(response)
                ?? throw new InvalidOperationException("No response from Finnhub server!");

            if (pairs.TryGetValue("error", out object? value))
            {
                throw new InvalidOperationException(value.ToString());
            }

            return pairs;
        }
    }
}