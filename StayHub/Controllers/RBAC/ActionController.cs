using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Action;
using StayHub.Application.CQRS.RBAC.Query.Action;

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
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await Mediator.Send(new GetAllActionQuery()));
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
