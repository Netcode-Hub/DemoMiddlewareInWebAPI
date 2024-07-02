
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DemoMiddlewareInWebAPI.Middlewares
{
    public class AuthorizationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var authState = context.Request.Headers["AuthState"].FirstOrDefault();
            if (Equals(authState, "Authenticated"))
            {
                await next(context);
            }
            else
            {
                var error = new ProblemDetails()
                {
                    Title = $"AuthState={authState}",
                    Status = StatusCodes.Status401Unauthorized,
                    Detail = "You are not allowed to access"
                };
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
                return;
            }
        }
    }
}
