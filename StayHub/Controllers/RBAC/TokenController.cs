using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Action;
using StayHub.Application.CQRS.RBAC.Command.Token;

namespace StayHub.API.Controllers.RBAC
{
    [AllowAnonymous]
    public class TokenController :BaseController
    {
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
    }
}
