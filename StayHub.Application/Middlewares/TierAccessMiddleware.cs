using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Repository.TMS;

public class TierAccessMiddleware
{
    private readonly RequestDelegate _next;

    public TierAccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    // todo: Check user in property/unit, check property(unit) tier -> allowed actions, check subscription end
    public async Task InvokeAsync(HttpContext context, IPropertyRepository propertyRepository,IUserRepository userRepositor,ITierRepository tierRepository)
    {
        var (method, action) = context.GetRouteInfo();
        var routeData = context.GetRouteData();
        var propertyIdValue = routeData.Values["propertyId"]?.ToString();
        var unitIdValue = routeData.Values["unitId"]?.ToString();
        var currentUser = context.User;
        int.TryParse(currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
        var hasProperty = int.TryParse(propertyIdValue, out var propertyId);
        var hasUnit = int.TryParse(propertyIdValue, out var unitId);
        // check supscrioption
        if (!((hasUnit && await tierRepository.IsUnitAlloweForActionAsync(unitId,action))||(hasProperty && await tierRepository.IsPropertyAlloweForActionAsync(propertyId,action))))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("End of subscription.");
            return; // Short-circuits the pipeline
        }
        if (hasUnit)
        {
            
        }
        else if (hasProperty)
        {
           // Check if User is associated with the Property/Unit
            var isUserInProperty = await propertyRepository.IsUserInPropertyAsync(userId, propertyId);
            if (!isUserInProperty)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { error = "Access denied: User not associated with this property" });
                return;
            }
            // Check tier    
            
        }

        await _next(context);
    }
}