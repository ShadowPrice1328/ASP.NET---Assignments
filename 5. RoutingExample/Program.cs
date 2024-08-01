var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Dictionary<int, string> dictionary = new()
{
    {1, "United States"},
    {2, "Canada"},
    {3, "United Kingdom"},
    {4, "India"},
    {5, "Japan"}
};

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    app.MapGet("/countries", async context =>
    {
        int id = Convert.ToInt32(context.Request.RouteValues["id"]);

        foreach (var kvp in dictionary)
        {
            await context.Response.WriteAsync($"{kvp.Key}, {kvp.Value}\n");
        }
    });

    app.MapGet("/countries/{id:int}", async context =>
    {
        int id = Convert.ToInt32(context.Request.RouteValues["id"]);

        if (id >= 1 && id <= 5)
        {
            string countryName = dictionary.GetValueOrDefault(id, "[No Country]");
            await context.Response.WriteAsync(countryName);
        }
        else if (id > 100)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("The CountryID should be between 1 and 100");
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("[No Country]");
        }
    });
});

app.Map("/", () => "Hello World!");

app.Run();