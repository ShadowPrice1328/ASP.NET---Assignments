using Social_Media_Links.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<SocialMediaLinksOptions>(builder.Configuration.GetSection("SocialMediaLinks"));

var app = builder.Build();

app.MapDefaultControllerRoute();
app.UseStaticFiles();

app.Run();