using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Query.Action
{
    public record GetAllActionOfRoleQuery(int RoleId) : IRequest<BaseResponse<List<ActionDTO>>>;
    public sealed class GetAllActionOfRoleQueryHandler(IRoleActionRepository roleActionRepository) : BaseResponseHandler, IRequestHandler<GetAllActionOfRoleQuery, BaseResponse<List<ActionDTO>>>
    {
        public async Task<BaseResponse<List<ActionDTO>>> Handle(GetAllActionOfRoleQuery request, CancellationToken cancellationToken)
        {
            return Success(data: (await roleActionRepository.GetAllActionOfRole(request.RoleId)).ToList());
        }
    }
}
