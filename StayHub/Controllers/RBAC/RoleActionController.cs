using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.MenuAction;
using StayHub.Application.CQRS.RBAC.Command.RoleAction;
using StayHub.Application.CQRS.RBAC.Command.UserRole;
using StayHub.Application.CQRS.RBAC.Query.Action;
using StayHub.Application.CQRS.RBAC.Query.User;

namespace StayHub.API.Controllers.RBAC
{
    public class RoleActionController : BaseController
    {
        [HttpPost("role/assign-action")]
        public async Task<IActionResult> AssignRoleToAction(AssignActionsToRoleCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
        [HttpGet("action-of-role/{id}")]
        public async Task<IActionResult> GetActionOfRole(int id)
        {
            return GenerateResponse(await Mediator.Send(new GetAllActionOfRoleQuery(id)
           ));
        }
    }
}
