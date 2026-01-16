using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.MenuAction;
using StayHub.Application.CQRS.RBAC.Command.RoleMenu;
using StayHub.Application.CQRS.RBAC.Query.Menu;
using StayHub.Application.CQRS.RBAC.Query.Role;

namespace StayHub.API.Controllers.RBAC
{
    public class RoleMenuController : BaseController
    {
        [HttpGet("menu-of-role/{roleId}")]
        public async Task<IActionResult> GetMenuOfRole(int roleId)
        {
            return Ok(await Mediator.Send(new GetAllMenuOfRole(roleId)));
        }
        [HttpPost("role/assign-menu")]
        public async Task<IActionResult> AssignMenuToRole(AssignMenuToRoleCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
    }
}
