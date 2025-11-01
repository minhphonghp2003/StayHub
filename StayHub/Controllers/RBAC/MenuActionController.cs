using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.RoleAction;
using StayHub.Application.CQRS.RBAC.Query.Action;

namespace StayHub.API.Controllers.RBAC
{
    public class MenuActionController : BaseController
    {
        [HttpPost("menu/assignActionToMenu")]
        public async Task<IActionResult> AssignActionToMenu(AddMenuActionCommand request)
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
