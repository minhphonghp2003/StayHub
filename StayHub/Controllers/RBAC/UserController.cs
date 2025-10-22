using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Token;
using StayHub.Application.CQRS.RBAC.Query.User;

namespace StayHub.API.Controllers.RBAC
{
    public class UserController : BaseController
    {
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(GetProfileQuery query)
        {
            return GenerateResponse(await Mediator.Send(query));
        }

    }
}
