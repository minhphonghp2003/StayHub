using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Token;

namespace StayHub.API.Controllers.RBAC
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Success)
            {

                SetCookie("refresh", result.Data.RefreshToken, result.Data.ExpiresDate);
                SetCookie("access_token", result.Data.Token);
            }
            return GenerateResponse(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Success)
            {

                SetCookie("refresh", result.Data.RefreshToken, result.Data.ExpiresDate);
                SetCookie("access_token", result.Data.Token);
            }
            return GenerateResponse(result);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RevokeTokenCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Success)
            {
                DeleteCookie("refresh");
                DeleteCookie("access_token");
            }
            return GenerateResponse(result);

        }


    }
}
