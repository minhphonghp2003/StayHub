using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.RoleAction;

namespace StayHub.API.Controllers.RBAC
{
    public class MenuActionController : BaseController
    {
        [HttpPost("menu/assignActionToMenu")]
        public async Task<IActionResult> AssignActionToMenu(AddMenuActionCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
    }
}
