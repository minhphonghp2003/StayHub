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
            var existActions = (await menuActionRepository.GetManyAsync(ma => ma.MenuId == request.menuId && request.actionIds.Contains(ma.ActionId),
                selector: (e, i) => e.ActionId)).ToList();
            var newActionIds = request.actionIds.Except(existActions).ToList();
            var result = await menuActionRepository.AddRangeAsync(newActionIds.Select(aid => new StayHub.Domain.Entity.RBAC.MenuAction
            {
                ActionId = aid,
                MenuId = request.menuId
            }).ToList());

            if (result == null)
                return Failure<List<int>>("Failed to assign actions to menu.", System.Net.HttpStatusCode.OK);

            return Success(result.Select(e => e.ActionId).ToList(), "Actions assigned to menu successfully.", System.Net.HttpStatusCode.OK);
        }
    }

}