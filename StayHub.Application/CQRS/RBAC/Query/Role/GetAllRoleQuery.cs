using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Role
{
    // Include properties to be used as input for the query
    public record GetAllRoleQuery() : IRequest<BaseResponse<List<RoleDTO>>>;
    public sealed class GetAllRoleQueryHandler(IRoleRepository roleRepository) : BaseResponseHandler, IRequestHandler<GetAllRoleQuery, BaseResponse<List<RoleDTO>>>
    {
        public async Task<BaseResponse<List<RoleDTO>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await roleRepository.GetAllAsync(selector: (e, i) => new RoleDTO
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Description = e.Description,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            });
            return Success(data: result.ToList());
        }
    }

}
