using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace StayHub.Application.Middlewares
{
    public class AuthorizationMiddleware(RequestDelegate _next, ILogger<AuthorizationMiddleware> _logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var allowAnon = endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>();
            if (allowAnon != null)
            {
                await _next(context);
                return;
            }
            string routePattern = "";
            if (endpoint is RouteEndpoint routeEndpoint)
            {
                routePattern = routeEndpoint.RoutePattern.RawText;
            }
            if (string.IsNullOrWhiteSpace(routePattern))
            {
                await _next(context);
                return;
            }
            var method = context.Request.Method; 
            var currentUser = context.User;
            int.TryParse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);
            var roles = currentUser.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            // Check user permissions against required permissions for the endpoint
            await _next(context); // Call the next middleware in the pipeline
        }
    }
}
