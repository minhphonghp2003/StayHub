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
    public async Task InvokeAsync(HttpContext context, IPropertyRepository propertyRepository,IUserRepository userRepositor,ITierRepository tierRepository,IUnitRepository unitRepository) 
    {
        var (method, action) = context.GetRouteInfo();
        var routeData = context.GetRouteData();
        var propertyIdValue = routeData.Values["propertyId"]?.ToString();
        var unitIdValue = routeData.Values["unitId"]?.ToString();
        var currentUser = context.User;
        int.TryParse(currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
        var hasProperty = int.TryParse(propertyIdValue, out var propertyId);
        var hasUnit = int.TryParse(unitIdValue, out var unitId);
        // check for subscription 
        if((hasProperty && !await propertyRepository.IsSubscriptionActiveAsync(propertyId)) || (hasUnit && !await unitRepository.IsSubscriptionActiveAsync(unitId)))
        {
            context.Response.StatusCode = StatusCodes.Status402PaymentRequired;
            await context.Response.WriteAsync("Subscription for this resource is inactive. Please contact support.");
            return; // Short-circuits the pipeline
        }
        // check for allowance 
        if (action!=null  && ((hasUnit && !await tierRepository.IsUnitAlloweForActionAsync(unitId,method,action))||(hasProperty && !await tierRepository.IsPropertyAlloweForActionAsync(propertyId,method,action))))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Current tier does not allow access to this resource.");
            return; // Short-circuits the pipeline
        }
        // check for user association
        if (hasUnit && !(await unitRepository.IsUserInUnitAsync(userId, unitId)))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new { error = "Access denied: User not associated with this property" });
            return; 
        }
        if (hasProperty && !(await propertyRepository.IsUserInPropertyAsync(userId, propertyId)))
        {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { error = "Access denied: User not associated with this unit" });
                return;
            
        }

        await _next(context);
    }
}