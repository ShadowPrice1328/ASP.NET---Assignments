using Models;

namespace ServiceContracts
{
    public interface IFinnhubService
    {
        /// <summary>
        /// Retrieves the company profile.
        /// </summary>
        /// <returns>A class representing the company profile</returns>
        public Task<CompanyProfile> GetCompanyProfile(string? symbol);

        /// <summary>
        /// Retrieves the current stock price for specified symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>A class representing the price quote</returns>
        public Task<StockPriceQuote> GetStockPriceQuote(string? symbol);
    }
}
