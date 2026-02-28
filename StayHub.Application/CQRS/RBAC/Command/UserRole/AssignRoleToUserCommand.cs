using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common;
using Shared.Response;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.UserRole
{
    public record AssignRoleToUserCommand(int UserId, List<int> RoleIds) : IRequest<BaseResponse<List<int>>>;

    public sealed class AssignRoleToUserCommandHandler(
        IUserRepository userRepository,
        IHttpContextAccessor accessor,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository)
        : BaseResponseHandler, IRequestHandler<AssignRoleToUserCommand, BaseResponse<List<int>>>
    {
        public async Task<BaseResponse<List<int>>> Handle(AssignRoleToUserCommand request,
            CancellationToken cancellationToken)
        {
            var isSystemUser = await userRepository.IsSystemUser(accessor.HttpContext.GetUserId() ?? 0);
            var systemRole = Enum.GetNames(typeof(SystemRole)).ToList();
            var roles = await roleRepository.GetManyAsync(
                filter: e => request.RoleIds.Contains(e.Id) && (isSystemUser || !systemRole.Contains(e.Code)),
                selector: (e, i) => e.Id);
            await userRoleRepository.DeleteWhere(e => e.UserId == request.UserId, false);
            var result = await userRoleRepository.AddRangeAsync(roles.Select(rid =>
                new StayHub.Domain.Entity.RBAC.UserRole
                {
                    UserId = request.UserId,
                    RoleId = rid
                }).ToList());
            return Success<List<int>>(data: request.RoleIds.ToList(), message: "Role assigned to user successfully");
        }
    }
}