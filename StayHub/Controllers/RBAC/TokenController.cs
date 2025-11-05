using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Action;
using StayHub.Application.CQRS.RBAC.Command.Token;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StayHub.API.Controllers.RBAC
{
    [AllowAnonymous]
    public class TokenController : BaseController
    {
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand request)
        {
            var result = await Mediator.Send(request);
            if (result.Success)
            {

                SetCookie("refresh", result.Data.RefreshToken, result.Data.ExpiresDate);
                SetCookie("access_token", result.Data.Token);
            }
            return GenerateResponse(result);
        }
    }
}
