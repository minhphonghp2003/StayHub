using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Command.Token;
using StayHub.Application.CQRS.RBAC.Command.UserRole;
using StayHub.Application.CQRS.RBAC.Query.Role;
using StayHub.Application.CQRS.RBAC.Query.User;

namespace StayHub.API.Controllers.RBAC
{
    public class UserRoleController : BaseController
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
    }
}
