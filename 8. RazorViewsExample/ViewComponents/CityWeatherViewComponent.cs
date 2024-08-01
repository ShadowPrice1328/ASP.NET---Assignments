using Microsoft.AspNetCore.Mvc;
using ViewsExample.Models;

namespace ViewsExample.ViewComponents;

public class CityWeatherViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(CityWeather model)
    {
        ViewData["BoxColor"] = GetCssClassByFahrenheit(model.TemperatureFahrengeit);
        return View(model);
    }

    private string GetCssClassByFahrenheit(int TemperatureFahrenheit)
    {
        return TemperatureFahrenheit switch
        {
            (< 44) => "blue-back",
            (>= 44) and (< 75) => "green-back",
            (>= 75) => "orange-back"
        };
    }
}