using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Token;

namespace StayHub.API.Controllers.RBAC
{
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            return GenerateResponse(await Mediator.Send(command));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            return GenerateResponse(await Mediator.Send(command));
        }


    }
}
