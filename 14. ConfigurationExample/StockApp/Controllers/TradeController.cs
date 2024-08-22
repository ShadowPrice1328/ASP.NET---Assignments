using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockApp.ViewModels;

namespace StockApp.Controllers;

public class TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, IConfiguration configuration) : Controller
{
    private readonly TradingOptions _tradingOptions = tradingOptions.Value;
    private readonly IFinnhubService _finnhubService = finnhubService;
    private readonly IConfiguration _configuration = configuration;
    
    [Route("/")]
    [Route("[action]")]
    [Route("~/[controller]")]
    public async Task<IActionResult> Index()
    {
        string symbol = _tradingOptions.DefaultStockSymbol ?? "MSFT";

        Dictionary<string, object>? companyProfileDictionary = await _finnhubService.GetCompanyProfile(symbol);
        Dictionary<string, object>? stockPriceQuoteDictionary = await _finnhubService.GetStockPriceQuote(symbol);

        StockTrade stockTrade = new() {StockSymbol = _tradingOptions.DefaultStockSymbol};

        if (companyProfileDictionary != null && stockPriceQuoteDictionary != null)
        {
            stockTrade = new()
            {
                StockSymbol = companyProfileDictionary["ticker"].ToString(),
                StockName = companyProfileDictionary["name"].ToString(),
                Price = Convert.ToDouble(stockPriceQuoteDictionary["c"].ToString())
            };
        }

        ViewBag.Token = _configuration["Token"];

        return View(stockTrade);
    }
}