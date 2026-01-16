using Microsoft.AspNetCore.Mvc;
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

    }
}
