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
    public record GetRoleByIdQuery(int Id) : IRequest<BaseResponse<RoleDTO>>;
    internal sealed class GetRoleByIdQueryHandler(IRoleRepository roleRepository) : BaseResponseHandler, IRequestHandler<GetRoleByIdQuery, BaseResponse<RoleDTO>>
    {
        public async Task<BaseResponse<RoleDTO>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await roleRepository.FindOneAsync(filter: e => e.Id == request.Id, selector: (e) => new RoleDTO
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Description = e.Description,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            });
            if (result == null)
            {
                return Failure<RoleDTO>(message: "Role not found", code: System.Net.HttpStatusCode.BadRequest);
            }
            return Success<RoleDTO>(result);
        }
    }
}
