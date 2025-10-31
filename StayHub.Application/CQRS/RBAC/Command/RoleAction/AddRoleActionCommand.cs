using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.RoleAction
{
    // Include properties to be used as input for the command
    public record AddRoleActionCommand(int RoleId, int ActionId) : IRequest<BaseResponse<bool>>;
    public sealed class AddRoleActionCommandHandler(IRoleActionRepository roleActionRepository, IRoleRepository roleRepository, IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<AddRoleActionCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(AddRoleActionCommand request, CancellationToken cancellationToken)
        {
            if (!await roleRepository.ExistsAsync(request.RoleId))
            {
                return Failure<bool>($"Role with ID {request.RoleId} does not exist.", System.Net.HttpStatusCode.BadRequest);
            }
            if (!await actionRepository.ExistsAsync(request.ActionId))
            {
                return Failure<bool>($"Action with ID {request.ActionId} does not exist.", System.Net.HttpStatusCode.BadRequest);
            }

            if (await roleActionRepository.ExistsAsync(e=>e.RoleId==request.RoleId && e.ActionId==request.ActionId))
            {
                return Failure<bool>($"The action with ID {request.ActionId} is already assigned to the role with ID {request.RoleId}.", System.Net.HttpStatusCode.Conflict);
            }
            await roleActionRepository.AddAsync(new Domain.Entity.RBAC.RoleAction
            {
                RoleId = request.RoleId,
                ActionId = request.ActionId
            });
            return Success(true, "RoleAction added successfully.");
        }
    }

}