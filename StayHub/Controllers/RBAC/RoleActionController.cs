using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.RoleAction;
using StayHub.Application.CQRS.RBAC.Command.UserRole;

namespace StayHub.API.Controllers.RBAC
{
    public class RoleActionController :BaseController
    {
        [HttpPost("role/assignRoleToAction")]
        public async Task<IActionResult> AssignRoleToAction(AddRoleActionCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
    }
}
