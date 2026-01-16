using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Query.User
{
    public record GetAllUserOfRoleQuery(int roleId) : IRequest<BaseResponse<List<UserDTO>>>;
    public sealed class GetAllUserOfRoleQueryHandler(IUserRepository userRepository) : BaseResponseHandler, IRequestHandler<GetAllUserOfRoleQuery, BaseResponse<List<UserDTO>>>
    {
        public async Task<BaseResponse<List<UserDTO>>> Handle(GetAllUserOfRoleQuery request, CancellationToken cancellationToken)
        {
            return Success(data: (await userRepository.GetUserOfRole(request.roleId)));
        }
    }
}
