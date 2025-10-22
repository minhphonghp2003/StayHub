using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Token;
using StayHub.Application.CQRS.RBAC.Query.User;

namespace StayHub.API.Controllers.RBAC
{
    [Authorize]
    public class UserController : BaseController
    {
        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            return GenerateResponse(await Mediator.Send(new GetProfileQuery(id)
           ));
        }

    }
}
