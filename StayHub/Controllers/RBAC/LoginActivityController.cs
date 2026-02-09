using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Action;
using StayHub.Application.CQRS.RBAC.Command.Menu;
using StayHub.Application.CQRS.RBAC.Command.Token;
using StayHub.Application.CQRS.RBAC.Command.User;
using StayHub.Application.CQRS.RBAC.Query.Menu;
using StayHub.Application.CQRS.RBAC.Query.User;
using StayHub.Application.Extension;

namespace StayHub.API.Controllers.RBAC
{
    [Authorize]
    public class LoginActivityController : BaseController
    {
        [HttpGet("my")]
        public async Task<IActionResult> GetAllAsync( int? pageNumber = null, int? pageSize = null)
        {
            return Ok(await Mediator.Send(new GetAllActivityQuery(HttpContext.GetUserId().Value, pageNumber, pageSize)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserActivities(int id, int? pageNumber = null, int? pageSize = null)
        {
            return Ok(await Mediator.Send(new GetAllActivityQuery(id, pageNumber, pageSize)));
        }
    }
}
