using MediatR;
using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Action;
using StayHub.Application.CQRS.RBAC.Query.Action;

namespace StayHub.API.Controllers
{
    public class DemoController(IMediator mediator) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await mediator.Send(new GetAllActionQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAction(AddActionCommand request)
        {
            return Ok(await mediator.Send(request));
        }

    }
}
