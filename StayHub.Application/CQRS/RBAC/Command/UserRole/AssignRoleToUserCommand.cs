using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.UserRole
{
    // TODO prevent tenant from assigning system role to user
    public record AssignRoleToUserCommand(int UserId, List<int> RoleIds) : IRequest<BaseResponse<List<int>>>;
    public sealed class AssignRoleToUserCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository) : BaseResponseHandler, IRequestHandler<AssignRoleToUserCommand, BaseResponse<List<int>>>
    {
        public async Task<BaseResponse<List<int>>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            await userRoleRepository.DeleteWhere(e => e.UserId == request.UserId, false);
            var result = await userRoleRepository.AddRangeAsync(request.RoleIds.Select(rid => new StayHub.Domain.Entity.RBAC.UserRole
            {
                UserId = request.UserId,
                RoleId = rid
            }).ToList());
            return Success<List<int>>(data: request.RoleIds.ToList(), message: "Role assigned to user successfully");
        }
    }

}