using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Action;
using StayHub.Application.CQRS.RBAC.Command.Menu;
using StayHub.Application.CQRS.RBAC.Query.Action;
using StayHub.Application.CQRS.RBAC.Query.Menu;

namespace StayHub.API.Controllers.RBAC
{
    public class ActionController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            return Ok(await Mediator.Send(new GetActionByIdQuery(Id)));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(string? search = null, int? pageNumber = null, int? pageSize = null)
        {
            return Ok(await Mediator.Send(new GetAllActionQuery( pageNumber, pageSize, search)));
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllNoPaginatedAsync()
        {
            return Ok(await Mediator.Send(new GetAllActionNoPaginatedQuery()));
        }
        [HttpPatch("allow-anonymous/{id}")]
        public async Task<IActionResult> AllowAnon(int id, [FromQuery] bool allow)
        {

            return GenerateResponse(await Mediator.Send(new AllowAnonymousCommand(id, allow)));
        }
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateAllAction()
        {
            return GenerateResponse(await Mediator.Send(new GenerateAllActionCommand()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAction(AddActionCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UdpateAction(int id, UpdateActionCommand request)
        {
            request.Id = id;
            return GenerateResponse(await Mediator.Send(request));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAction(int id)
        {
            return GenerateResponse(await Mediator.Send(new DeleteActionCommand(id)));
        }


    }
}
