using System.Text.Json;
using ConfigurationExample.ServiceContracts;
using Microsoft.Extensions.Options;

namespace ConfigurationExample.Services;

public class FinnHubService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IFinnHubService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string symbol)
    {
        using (HttpClient httpClient = _httpClientFactory.CreateClient())
        {
            HttpRequestMessage httpRequestMessage = new()
            {
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration["FinnhubToken"]}"),
                Method = HttpMethod.Get
            };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            Stream stream = httpResponseMessage.Content.ReadAsStream();
            StreamReader streamReader = new(stream);

            string responseBody = streamReader.ReadToEnd();

            Dictionary<string, object>? response = JsonSerializer.Deserialize<Dictionary<string, object>?>(responseBody) 
                ?? throw new InvalidOperationException("No responce from FinnHub server");
                
            if (response.TryGetValue("error", out object? value))
            {
                throw new InvalidOperationException(value.ToString());
            }

            return response;
        }
    }
}