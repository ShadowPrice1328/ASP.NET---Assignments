using ConfigurationExample.Models;
using ConfigurationExample.ServiceContracts;
using ConfigurationExample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationExample.Controllers;

public class HomeController(IOptions<WeatherApiOptions> options, IOptions<TradingOptions> tradingOptions, IFinnHubService finnHubService) : Controller
{
    private readonly IOptions<TradingOptions> _tradingOptions = tradingOptions;
    private readonly IFinnHubService _finnHubService = finnHubService;
    private readonly WeatherApiOptions _weatherOptions = options.Value;

    [Route("/")]
    public IActionResult Index([FromServices] IWebHostEnvironment webHostEnvironment)
    {
        Tuple<string?, string?> tuple = 
            Tuple.Create<string?, string?>(_weatherOptions.ClientID, _weatherOptions.ClientSecret);

        ViewBag.Env = webHostEnvironment.EnvironmentName;

        return View(tuple);
    }
    [Route("/finance")]
    public async Task<IActionResult> Finance()
    {
        if (string.IsNullOrEmpty(_tradingOptions.Value.DefaultStockSymbol))
        {
            _tradingOptions.Value.DefaultStockSymbol = "MSFT";
        }
        Dictionary<string, object>? model = 
            await _finnHubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);

        Stock stock = new()
        {
            StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
            CurrentPrice = Convert.ToDouble(model["c"].ToString()),
            HighPrice = Convert.ToDouble(model["h"].ToString()),
            LowPrice = Convert.ToDouble(model["l"].ToString()),
            OpenPrice = Convert.ToDouble(model["o"].ToString())
        };

        return View(stock);
    }
}