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
    public record GetAllUserOfRoleQuery(int roleId) : IRequest<BaseResponse<List<int>>>;
    public sealed class GetAllUserOfRoleQueryHandler(IUserRoleRepository userRoleRepository) : BaseResponseHandler, IRequestHandler<GetAllUserOfRoleQuery, BaseResponse<List<int>>>
    {
        public async Task<BaseResponse<List<int>>> Handle(GetAllUserOfRoleQuery request, CancellationToken cancellationToken)
        {
            return Success(data: (await userRoleRepository.GetManyAsync(filter:e=>e.RoleId==request.roleId,selector:(e,i)=>e.UserId)).ToList());
        }
    }
}
