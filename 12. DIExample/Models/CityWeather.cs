namespace Models
{
    public class CityWeather
    {
        public string CityUniqueCode { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public DateTime DateAndTime { get; set; }
        public int TemperatureFahrenheit { get; set; }
    }
}
