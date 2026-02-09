using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.Common.Command.Email;
using StayHub.Application.CQRS.RBAC.Command.Token;
using StayHub.Application.CQRS.RBAC.Command.User;
using StayHub.Application.Extension;
using StayHub.Domain.Entity.RBAC;

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

        [HttpPost("revoke-all-token/{id}")]
        public async Task<IActionResult> RevokeAllToken(int id)
        {
            var result = await Mediator.Send(new RevokeAllUserTokenCommand(id));
            return GenerateResponse(result);

        }
        [HttpPost("forget-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return GenerateResponse(result);

        }
        
        [HttpPost("new-password")]
        [AllowAnonymous]
        public async Task<IActionResult> NewPassword(NewPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return GenerateResponse(result);

        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            command.userId = HttpContext.GetUserId() ?? 0;
            var result = await Mediator.Send(command);
            return GenerateResponse(result);

        }
      
        [HttpPost("change-tenant-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangeTenantPassword(ChangeTenantPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return GenerateResponse(result);

        }
    }
}
