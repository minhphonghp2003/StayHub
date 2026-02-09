using Microsoft.AspNetCore.Http;
using StayHub.Domain.Entity.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        public static string? GetIp(this HttpContext context)   =>
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
    }
}
