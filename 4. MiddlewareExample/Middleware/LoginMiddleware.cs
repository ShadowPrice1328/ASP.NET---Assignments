
namespace MiddlewareExample.Middleware
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "POST")
            {
                var email = context.Request.Query["email"];
                var password = context.Request.Query["password"];

                if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
                {
                    if (email == "admin@example.com" && password == "admin1234")
                    {
                        await context.Response.WriteAsync("Succesfull login.\n");
                    }
                    else if (email != "admin@example.com" || password != "admin1234")
                    {
                        await context.Response.WriteAsync("Invalid login.\n");
                    }
                }
                if (string.IsNullOrWhiteSpace(email))
                {
                    await context.Response.WriteAsync("Invalid input for 'email'\n");
                }
                if (string.IsNullOrWhiteSpace(password))
                {
                    await context.Response.WriteAsync("Invalid input for 'password'\n");
                }
            }
            else
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("No response\n");
            }
        }
    }
    public static class LoginMiddlewareExtentions
    {
        public static IApplicationBuilder UseLogin(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}