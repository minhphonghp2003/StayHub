using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Token;
using StayHub.Application.CQRS.RBAC.Command.UserRole;

namespace StayHub.API.Controllers.RBAC
{
    public class UserRoleController: BaseController
    {
        [HttpPost("user/assignRole")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
    }
}
