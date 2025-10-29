using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Action;
using StayHub.Application.CQRS.RBAC.Query.Action;

namespace StayHub.API.Controllers.RBAC
{
    public class ActionController : BaseController
    {
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
    }
}
