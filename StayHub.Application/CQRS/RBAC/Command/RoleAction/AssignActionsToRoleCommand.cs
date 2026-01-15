using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.MenuAction
{
    // Include properties to be used as input for the command
    public record AssignActionsToRoleCommand(List<int> actionIds, int roleId) : IRequest<Response<List<int>>>;
    public sealed class AssignActionsToRoleCommandHandler(IRoleActionRepository roleActionRepository) : BaseResponseHandler, IRequestHandler<AssignActionsToRoleCommand, Response<List<int>>>
    {
        public async Task<Response<List<int>>> Handle(AssignActionsToRoleCommand request, CancellationToken cancellationToken)
        {
            await roleActionRepository.DeleteWhere(e => e.RoleId == request.roleId, false);
            var result = await roleActionRepository.AddRangeAsync(request.actionIds.Select(aid => new StayHub.Domain.Entity.RBAC.RoleAction
            {
                ActionId = aid,
                RoleId = request.roleId
            }).ToList());

            if (result == null)
                return Failure<List<int>>("Failed to assign actions to role.", System.Net.HttpStatusCode.OK);

            return Success(request.actionIds, "Actions assigned to role successfully.", System.Net.HttpStatusCode.OK);
        }
    }

}