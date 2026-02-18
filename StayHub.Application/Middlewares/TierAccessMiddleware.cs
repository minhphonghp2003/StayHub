using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StayHub.Application.Interfaces.Repository.TMS;

public class TierAccessMiddleware
{
    private readonly RequestDelegate _next;

    public TierAccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    // todo: Check user in property/unit, check property(unit) tier -> allowed actions, check subscription end
    public async Task InvokeAsync(HttpContext context, IPropertyRepository propertyRepository)
    {
        // 1. Extract ID from Path (e.g., /api/properties/{id}/...)
        var routeData = context.GetRouteData();
        var idValue = routeData.Values["propertyId"]?.ToString();

        if (Guid.TryParse(idValue, out var propertyId))
        {
            // 2. Fetch Property + Tier + Allowed Actions
            // var propertyInfo = await dbContext.Properties
            //     .Where(p => p.Id == propertyId)
            //     .Select(p => new {
            //         p.Id,
            //         p.TierId,
            //         p.SubscriptionEnd,
            //         AllowedActions = p.Tier.TierMenus.Select(m => m.ActionCode).ToList()
            //     })
            //     .FirstOrDefaultAsync();
            //
            // if (propertyInfo == null)
            // {
            //     context.Response.StatusCode = StatusCodes.Status404NotFound;
            //     await context.Response.WriteAsJsonAsync(new { error = "Property not found" });
            //     return;
            // }
            //
            // // 3. Check Subscription Expiry
            // if (propertyInfo.SubscriptionEnd < DateTime.UtcNow)
            // {
            //     context.Response.StatusCode = StatusCodes.Status402PaymentRequired;
            //     await context.Response.WriteAsJsonAsync(new { error = "Subscription expired" });
            //     return;
            // }
            //
            // // 4. Store Info in HttpContext.Items for the Controller to use
            // context.Items["PropertyActions"] = propertyInfo.AllowedActions;
            // context.Items["CurrentPropertyId"] = propertyInfo.Id;
        }

        await _next(context);
    }
}