using Microsoft.AspNetCore.Mvc;
using ViewsExample.Models;
namespace ViewsExample.Controllers;

public class WeatherController : Controller
{
    readonly List<CityWeather> cityWeathers = new()
    {
        new() {CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"), TemperatureFahrengeit = 33},
        new() {CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"), TemperatureFahrengeit = 60},
        new() {CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"), TemperatureFahrengeit = 82}
    };

    [Route("/")]
    public IActionResult Index()
    {
        return View("All", cityWeathers);
    }
    [Route("weather/{cityCode}")]
    public IActionResult Details(string cityCode)
    {
        CityWeather? cityWeather = cityWeathers.Where(w => w.CityUniqueCode == cityCode).FirstOrDefault();

        if (cityWeather == null)
        {
            return View("404");
        }

        return View(cityWeather);
    }
}