using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.MenuAction
{
    // Include properties to be used as input for the command
    public record AssignActionsToMenuCommand(List<int> actionIds, int menuId) : IRequest<Response<List<int>>>;
    public sealed class AssignActionsToMenuCommandHandler(IMenuActionRepository menuActionRepository) : BaseResponseHandler, IRequestHandler<AssignActionsToMenuCommand, Response<List<int>>>
    {
        public async Task<Response<List<int>>> Handle(AssignActionsToMenuCommand request, CancellationToken cancellationToken)
        {
            await menuActionRepository.DeleteWhere(e => e.MenuId == request.menuId,false);
             var result = await menuActionRepository.AddRangeAsync(request.actionIds.Select(aid => new StayHub.Domain.Entity.RBAC.MenuAction
            {
                ActionId = aid,
                MenuId = request.menuId
            }).ToList());

            if (result == null)
                return Failure<List<int>>("Failed to assign actions to menu.", System.Net.HttpStatusCode.OK);

            return Success(request.actionIds, "Actions assigned to menu successfully.", System.Net.HttpStatusCode.OK);
        }
    }

}