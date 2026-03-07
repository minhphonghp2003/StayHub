using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StayHub.Domain.Entity.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using uaParserLibrary;

namespace StayHub.Application.Extension
{
    public static class HttpContextExtensions
    {
        public static int? GetUserId(this HttpContext context)
            => int.Parse(context.User?.FindFirstValue(ClaimTypes.NameIdentifier));

        public static string? GetUserName(this HttpContext context)
            => context.User?.FindFirstValue(ClaimTypes.Name);

        public static string? GetEmail(this HttpContext context)
            => context.User?.FindFirstValue(ClaimTypes.Email);

        public static List<string>? GetRoles(this HttpContext context)
            => context.User?.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        public static string? GetIp(this HttpContext context) =>
            context.Connection.RemoteIpAddress?.ToString();

        public static string GetBrowser(this HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            if (string.IsNullOrEmpty(userAgent)) return "Unknown";

            var clientInfo = UAParser.GetBrowser(userAgent);
            return $"{clientInfo.Name} {clientInfo.Major}"; // e.g., "Chrome 120"
        }

        public static string GetOS(this HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            if (string.IsNullOrEmpty(userAgent)) return "Unknown";

            var clientInfo = UAParser.GetOS(userAgent);
            return $"{clientInfo.Name} {clientInfo.Version}"; // e.g., "Windows 10" or "iOS 17"
        }

        public static (string method, string? routePattern) GetRouteInfo(this HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint is RouteEndpoint routeEndpoint)
            {
                var routePattern = routeEndpoint.RoutePattern.RawText;
                var method = context.Request.Method;
                return (method, routePattern);
            }

            return ("Unknown", "Unknown");
        }
        public static async Task<(string? PropertyId, string? UnitId)> GetPropertyAndUnitFromReq(this HttpContext context)
        {
            // 1. Try to get from Route Data (e.g., /api/property/123)
            string? propertyId = context.GetRouteValue("propertyId")?.ToString();
            string? unitId = context.GetRouteValue("unitId")?.ToString();

            // 2. Fallback to Query String (e.g., /api/search?propertyId=123)
            propertyId ??= context.Request.Query["propertyId"].FirstOrDefault();
            unitId ??= context.Request.Query["unitId"].FirstOrDefault();

            // 3. Fallback to Request Body if still null and it's a POST/PUT/PATCH
            if ((propertyId == null || unitId == null) && context.Request.ContentLength > 0)
            {
                context.Request.EnableBuffering();

                using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0; // Reset for Controller

                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        try
                        {
                            using var jsonDoc = JsonDocument.Parse(body);
                            var root = jsonDoc.RootElement;

                            // Only overwrite if the route/query didn't already find it
                            propertyId ??= root.TryGetProperty("propertyId", out var p) ? p.ToString() : null;
                            unitId ??= root.TryGetProperty("unitId", out var u) ? u.ToString() : null;
                        }
                        catch (JsonException) { /* Handle or log malformed JSON */ }
                    }
                }
            }

            return (propertyId, unitId);
        }
    }
}
