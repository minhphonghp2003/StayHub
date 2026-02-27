using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Role
{
    // Include properties to be used as input for the query
    public record GetAllRoleQuery() : IRequest<BaseResponse<List<RoleDTO>>>;

    public sealed class GetAllRoleQueryHandler(IRoleRepository roleRepository, IHttpContextAccessor accessor)
        : BaseResponseHandler, IRequestHandler<GetAllRoleQuery, BaseResponse<List<RoleDTO>>>
    {
        public async Task<BaseResponse<List<RoleDTO>>> Handle(GetAllRoleQuery request,
            CancellationToken cancellationToken)
        {
            var userId = accessor.HttpContext.GetUserId();
            var userRoles = await roleRepository.GetManyAsync(filter: e => e.UserRoles.Any(e => e.UserId == userId),
                selector: (e, i) => e.Code);
            var systemRoles = Enum.GetNames(typeof(SystemRole)).ToList();
            var isAdmin = systemRoles.Any(e => userRoles.Contains(e));
            var result = await roleRepository.GetManyAsync(filter:e=> isAdmin || systemRoles.All(sr => sr != e.Code), selector: (e, i) => new RoleDTO
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