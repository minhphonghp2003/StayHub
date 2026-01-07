using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.MenuAction;
using StayHub.Application.CQRS.RBAC.Command.RoleAction;
using StayHub.Application.CQRS.RBAC.Query.Action;

namespace StayHub.API.Controllers.RBAC
{
    public class MenuActionController : BaseController
    {
        [HttpPost("menu/assign-action")]
        public async Task<IActionResult> AssignActionToMenu(AssignActionsToMenuCommand   request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
        [HttpGet("action-of-menu/{id}")]
        public async Task<IActionResult> GetActionOfMenu(int id)
        {
            return GenerateResponse(await Mediator.Send(new GetAllActionOfMenuQuery(id)
           ));
        }
    }
}
