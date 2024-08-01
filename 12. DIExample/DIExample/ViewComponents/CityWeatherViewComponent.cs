using Microsoft.AspNetCore.Mvc;
using Models;

namespace DIExample.ViewComponents
{
    public class CityWeatherViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CityWeather model)
        {
            ViewBag.BoxColor = GetBoxColor(model.TemperatureFahrenheit);
            return View(model);
        }
        private string GetBoxColor(int Temperature)
        {
            return Temperature switch
            {
                (< 44) => "blue-back",
                (>= 44) and (< 75) => "green-back",
                (>= 75) => "orange-back"
            };
        }
    }
}
