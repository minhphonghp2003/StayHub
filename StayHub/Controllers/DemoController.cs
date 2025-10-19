using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Action;
using StayHub.Application.CQRS.RBAC.Query.Action;

namespace StayHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController(IMediator mediator) : ControllerBase
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
