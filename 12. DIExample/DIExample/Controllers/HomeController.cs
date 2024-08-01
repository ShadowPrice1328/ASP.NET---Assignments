using Microsoft.AspNetCore.Mvc;
using Models;
using ServiceContracts;
using System.Reflection.Metadata.Ecma335;

namespace DIExample.Controllers
{
    public class HomeController(IWeatherService weatherService) : Controller
    {
        private readonly IWeatherService _weatherService = weatherService;

        [Route("/")]
        public IActionResult Index()
        {
            List<CityWeather> cities = _weatherService.GetWeatherDetails();
            return View(cities);
        }
        [Route("weather/{cityCode}")]
        public IActionResult Details(string cityCode) 
        {
            CityWeather? model = _weatherService.GetWeatherByCityCode(cityCode);

            if (model == null)
            {
                return View("404");
            }

            return View(model);
        }
    }
}
