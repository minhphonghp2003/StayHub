using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Menu;
using StayHub.Application.CQRS.RBAC.Command.Role;
using StayHub.Application.CQRS.RBAC.Query.Menu;
using StayHub.Application.CQRS.RBAC.Query.Role;

namespace StayHub.API.Controllers.RBAC
{
    public class RoleController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateMenu(AddRoleCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await Mediator.Send(new GetAllRoleQuery()));
        }



    }
}
