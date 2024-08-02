var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.Map("/", async context =>
//    {
//        await context.Response.WriteAsync(app.Configuration.GetValue("MyKey", "No key value!"));
//        await context.Response.WriteAsync(app.Configuration.GetSection("API")["ClientID"]);
//    });
//});

// app.MapGet("/", () => "Hello World!");

app.Run();
