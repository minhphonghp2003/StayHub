using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace StayHub.Application.Middlewares
{
    public class AuthorizationMiddleware(RequestDelegate _next, ILogger<AuthorizationMiddleware> _logger)
    {
        public async Task InvokeAsync(HttpContext context, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            var currentUser = context.User;
            int.TryParse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);
            var user =await userRepository.GetEntityByIdAsync(userId);
            if (!user.IsActive)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized access.");
                return; // Short-circuits the pipeline

            }
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
            if (!(await roleRepository.HasAccessToAction(userId, routePattern, method)))
            {

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized access.");
                return; // Short-circuits the pipeline
            }
            await _next(context); // Call the next middleware in the pipeline
        }
    }
}
