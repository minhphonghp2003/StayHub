using Microsoft.AspNetCore.Http;
using StayHub.Domain.Entity.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
    }
}
