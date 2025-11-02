using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Token;
using StayHub.Application.CQRS.RBAC.Query.User;
using StayHub.Application.Extension;

namespace StayHub.API.Controllers.RBAC
{
    [Authorize]
    public class UserController : BaseController
    {
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

    }
}
