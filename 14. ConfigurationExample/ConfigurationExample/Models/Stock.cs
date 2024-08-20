namespace ConfigurationExample.Models;

public class Stock
{
    public string? StockSymbol {get; set;}
    public double CurrentPrice {get; set;}
    public double HighPrice {get; set;}
    public double LowPrice {get; set;}
    public double OpenPrice {get; set;}
}