using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Action;
using StayHub.Application.CQRS.RBAC.Command.Menu;
using StayHub.Application.CQRS.RBAC.Query.Action;
using StayHub.Application.CQRS.RBAC.Query.Menu;

namespace StayHub.API.Controllers.RBAC
{
    public class MenuController:BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateMenu(AddMenuCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await Mediator.Send(new GetAllMenuQuery()));
        }
    }
}
