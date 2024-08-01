
namespace MiddlewareExample.Middleware
{
    public class RequestPersonMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestPersonMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var name = context.Request.Query["name"];
            context.Response.ContentType = "text/plain; charset=utf-8";
            
            if(!string.IsNullOrWhiteSpace(name))
            {
                await context.Response.WriteAsync($"Your name is {name}\n");
            }

            await _next(context);
        }
    }
    public static class RequestPersonMiddlewareExtentions
    {
        public static IApplicationBuilder UsePersonGreeting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestPersonMiddleware>();
        }
    }
}