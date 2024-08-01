using Microsoft.AspNetCore.Mvc;

namespace ViewsExample.Models;

public class CityWeather
{
    public required string CityUniqueCode {get; set;}
    public string? CityName {get; set;}
    public DateTime DateAndTime {get; set;}
    public int TemperatureFahrengeit {get; set;}
}