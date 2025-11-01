using Microsoft.AspNetCore.Mvc;
using Shared.Response;
using StayHub.Application.CQRS.RBAC.Command.Token;
using StayHub.Application.CQRS.RBAC.Command.UserRole;
using StayHub.Application.CQRS.RBAC.Query.Role;
using StayHub.Application.CQRS.RBAC.Query.User;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Extension;

namespace StayHub.API.Controllers.RBAC
{
    public class UserRoleController() : BaseController
    {
        [HttpPost("user/assignRole")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }
        [HttpGet("role-of-user/{id}")]
        public async Task<IActionResult> GetRoleOfUser(int id)
        {
            return GenerateResponse(await Mediator.Send(new GetAllRoleOfUserQuery(id)
           ));
        }
        [HttpGet("my")]
        public async Task<IActionResult> GetMyRole()
        {
            int? id = HttpContext.GetUserId();
            if (id == null)
            {
                return GenerateResponse(new BaseResponse<RoleDTO> { Data = null, Message = "Unauthorized", StatusCode = System.Net.HttpStatusCode.Unauthorized });
            }
            return GenerateResponse(await Mediator.Send(new GetAllRoleOfUserQuery((int)id)
           ));
        }
    }
}
