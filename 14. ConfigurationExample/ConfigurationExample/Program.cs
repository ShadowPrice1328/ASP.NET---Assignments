using ConfigurationExample;
using ConfigurationExample.ServiceContracts;
using ConfigurationExample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("API"));
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinnHubService, FinnHubService>();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("DefaultStockSymbol"));
builder.Configuration.AddJsonFile("MyOwnConfig.json", true, true);

//builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
//{
//    config.AddJsonFile("MyOwnConfig.json", true, true);
//});

var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
