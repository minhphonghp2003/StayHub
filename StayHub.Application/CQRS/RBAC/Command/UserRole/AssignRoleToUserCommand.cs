using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.UserRole
{
    // Include properties to be used as input for the command
    public record AssignRoleToUserCommand(int UserId, int RoleId) : IRequest<BaseResponse<bool>>;
    public sealed class AssignRoleToUserCommandHandler(IUserRepository userRepository,IUserRoleRepository userRoleRepository) : BaseResponseHandler, IRequestHandler<AssignRoleToUserCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return Failure<bool>(message: "No user found", code: System.Net.HttpStatusCode.BadRequest);
            }
            if ( await userRoleRepository.ExistsAsync(e=>e.UserId ==request.UserId && e.RoleId==request.RoleId))
            {

                return Failure<bool>(message: "Role already assigned to user", code: System.Net.HttpStatusCode.BadRequest);
            }
            await userRoleRepository.AddAsync(new Domain.Entity.RBAC.UserRole
            {
                UserId = request.UserId,
                RoleId = request.RoleId
            });
            return Success<bool>(data: true,message:"Role assigned to user successfully");
        }
    }

}