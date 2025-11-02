using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Action;
using StayHub.Application.CQRS.RBAC.Command.Menu;
using StayHub.Application.CQRS.RBAC.Command.Role;
using StayHub.Application.CQRS.RBAC.Query.Action;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            return Ok(await Mediator.Send(new GetRoleByIdQuery(Id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UdpateRole(int id, UpdateRoleCommand request)
        {
            request.Id = id;
            return GenerateResponse(await Mediator.Send(request));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            return GenerateResponse(await Mediator.Send(new DeleteRoleCommand(id)));
        }

    }
}
