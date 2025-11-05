using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;
using System.Net;

namespace StayHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        private IMediator _mediatorInstance;
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        public IActionResult GenerateResponse<T>(BaseResponse<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response);
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.Created:
                    return CreatedAtAction(null, response);
                default:
                    return Ok(response);
            }
        }
        private bool IsHttps => Request.Headers["X-Forwarded-Proto"].ToString().Equals("https", StringComparison.OrdinalIgnoreCase)
            || Request.Headers["Origin"].ToString().StartsWith("https", StringComparison.OrdinalIgnoreCase);
        protected void SetCookie(string cookieKey, string cookieValue, DateTime? expires = null, string? cookieDomain = null)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Expires = expires,
                Secure = IsHttps,
                SameSite = SameSiteMode.Lax,
            };

            if (!string.IsNullOrEmpty(cookieDomain))
            {
                cookieOptions.Domain = cookieDomain;
            }
            if (!IsHttps || Request.Headers.Origin.ToString().Contains("localhost"))
            {
                cookieOptions.Secure = true;
                cookieOptions.SameSite = SameSiteMode.None;
            }
            Response.Cookies.Append(cookieKey, cookieValue, cookieOptions);
        }
        protected void DeleteCookie(string cookieKey, string? cookieDomain =null)
        {
            var options = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1), // Set expiration to a past date
                SameSite = SameSiteMode.None, // Required for cross-site cookies
                Secure = IsHttps, // Required when SameSite is None
            };
            if (!string.IsNullOrEmpty(cookieDomain))
            {
                options.Domain = cookieDomain;
            }
            if (!IsHttps || Request.Headers.Origin.ToString().Contains("localhost"))
            {
                options.Secure = true;
                options.SameSite = SameSiteMode.None;
            }
            Response.Cookies.Delete(cookieKey, options);

        }

    }
}
