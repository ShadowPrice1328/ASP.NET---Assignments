using Microsoft.AspNetCore.Mvc;

namespace ConfigurationExample.Controllers;

public class HomeController(IConfiguration configuration) : Controller
{
    private readonly IConfiguration _configuration = configuration;

    [Route("/")]
    public IActionResult Index()
    {
        IConfigurationSection section = _configuration.GetSection("API");

        Tuple<string, string> tuple = 
            new(section.GetValue("ClientID", "No ClientID!"), section.GetValue("ClientSecret", "No ClientSecret!"));

        return View(tuple);
    }
}