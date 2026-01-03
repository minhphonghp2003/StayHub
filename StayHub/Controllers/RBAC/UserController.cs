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
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(string? search = null, int? pageNumber = null, int? pageSize = null)
        {
            return Ok(await Mediator.Send(new GetAllUserQuery(pageNumber, pageSize, search)));
        }
        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            return GenerateResponse(await Mediator.Send(new GetUserByIdQuery(id)
           ));
        }
        [HttpGet("my-profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            int? id = HttpContext.GetUserId();
            if (id == null)
            {
                return GenerateResponse(new Shared.Response.BaseResponse<object> { Data = null, Message = "Unauthorized", StatusCode = System.Net.HttpStatusCode.Unauthorized });
            }
            return GenerateResponse(await Mediator.Send(new GetUserByIdQuery((int)id)
           ));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UdpateUser(int id, UpdateUserCommand request)
        {
            request.Id = id;
            return GenerateResponse(await Mediator.Send(request));
        }

        [HttpPatch("set-activated/{id}")]
        public async Task<IActionResult> SetActivate(int id, [FromQuery] bool activated)
        {

            return GenerateResponse(await Mediator.Send(new SetActivateUserCommand(id, activated)));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return GenerateResponse(await Mediator.Send(new DeleteUserCommand(id)));
        }


    }
}
