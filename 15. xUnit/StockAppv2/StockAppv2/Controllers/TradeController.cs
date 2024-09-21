using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using Models;
using StockAppv2.ViewModels;

namespace StockAppv2.Controllers
{
    public class TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> options) : Controller
    {
        private readonly IFinnhubService _finnhubService = finnhubService;
        private readonly IOptions<TradingOptions> _options = options;

        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public async Task<IActionResult> Index()
        {
            string? stockSymbol = _options.Value.DefaultStockSymbol ?? "MSFT";

            CompanyProfile companyProfile = await _finnhubService.GetCompanyProfile(stockSymbol);
            StockPriceQuote stockPriceQuote = await _finnhubService.GetStockPriceQuote(stockSymbol);

            StockTradeViewModel viewModel = new()
            {
                StockName = companyProfile.Name,
                StockSymbol = stockSymbol,
                Price = stockPriceQuote.CurrentPrice,
            };

            return View(viewModel);
        }
    }
}
