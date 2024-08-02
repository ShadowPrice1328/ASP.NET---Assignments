using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationExample.Controllers;

public class HomeController(IOptions<WeatherApiOptions> options) : Controller
{
    private readonly WeatherApiOptions _options = options.Value;

    [Route("/")]
    public IActionResult Index()
    {
        Tuple<string, string> tuple = 
            Tuple.Create<string, string>(_options.ClientID, _options.ClientSecret);

        return View(tuple);
    }
}