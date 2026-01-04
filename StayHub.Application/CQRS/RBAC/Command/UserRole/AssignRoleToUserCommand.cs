using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.UserRole
{
    // Include properties to be used as input for the command
    public record AssignRoleToUserCommand(int UserId, List<int> RoleId) : IRequest<BaseResponse<List<int>>>;
    public sealed class AssignRoleToUserCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository) : BaseResponseHandler, IRequestHandler<AssignRoleToUserCommand, BaseResponse<List<int>>>
    {
        public async Task<BaseResponse<List<int>>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetEntityByIdAsync(request.UserId);
            if (user == null)
            {
                return Failure<List<int>>(message: "No user found", code: System.Net.HttpStatusCode.BadRequest);
            }
            var existRoles = (await userRoleRepository.GetManyAsync(e => e.UserId == request.UserId && request.RoleId.Contains(e.RoleId),
                selector: (e, i) => e.RoleId)).ToList();
            var newRoles = request.RoleId.Except(existRoles).ToList();
            var result = await userRoleRepository.AddRangeAsync(newRoles.Select(rid => new Domain.Entity.RBAC.UserRole
            {
                UserId = request.UserId,
                RoleId = rid
            }).ToList());
            return Success<List<int>>(data: result.Select(e => e.RoleId).ToList(), message: "Role assigned to user successfully");
        }
    }

}