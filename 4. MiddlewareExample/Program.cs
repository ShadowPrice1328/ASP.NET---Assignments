using MiddlewareExample.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

app.UseLogin();

app.Run(async context =>
{
    await context.Response.WriteAsync("Enter login and password in query parameters.");
});

app.Run();