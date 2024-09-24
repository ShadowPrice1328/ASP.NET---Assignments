using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IPersonsService, PersonsServices>();

var app = builder.Build();
app.UseStaticFiles();

app.MapDefaultControllerRoute();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Run();
