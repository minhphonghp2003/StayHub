using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Query.Role
{
    public record GetAllRoleOfUserQuery(int UserId) : IRequest<BaseResponse<List<RoleDTO>>>;
    public sealed class GetAllRoleOfUserQueryHandler(IUserRoleRepository userRoleRepository) : BaseResponseHandler, IRequestHandler<GetAllRoleOfUserQuery, BaseResponse<List<RoleDTO>>>
    {
        public async Task<BaseResponse<List<RoleDTO>>> Handle(GetAllRoleOfUserQuery request, CancellationToken cancellationToken)
        {
            return Success(data: (await userRoleRepository.GetAllRolesOfUser(request.UserId)).ToList());
        }
    }
}
