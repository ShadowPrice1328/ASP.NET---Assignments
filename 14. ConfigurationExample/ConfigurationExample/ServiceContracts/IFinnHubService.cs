namespace ConfigurationExample.ServiceContracts;

public interface IFinnHubService
{
    Task<Dictionary<string, object>?> GetStockPriceQuote(string symbol);
}