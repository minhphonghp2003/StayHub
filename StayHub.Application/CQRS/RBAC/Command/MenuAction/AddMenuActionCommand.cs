using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.RoleAction
{
    // Include properties to be used as input for the command
    public record AddMenuActionCommand(int MenuId, int ActionId) : IRequest<BaseResponse<bool>>;
    public sealed class AddMenuActionCommandHandler(IMenuActionRepository menuActionRepository,  IMenuRepository menuRepository, IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<AddMenuActionCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(AddMenuActionCommand request, CancellationToken cancellationToken)
        {
            if (!await menuRepository.ExistsAsync(request.MenuId))
            {
                return Failure<bool>($"Menu with ID {request.MenuId} does not exist.", System.Net.HttpStatusCode.BadRequest);
            }
            if (!await actionRepository.ExistsAsync(request.ActionId))
            {
                return Failure<bool>($"Action with ID {request.ActionId} does not exist.", System.Net.HttpStatusCode.BadRequest);
            }

            if (await menuActionRepository.ExistsAsync(e=>e.MenuId==request.MenuId && e.ActionId==request.ActionId))
            {
                return Failure<bool>($"The action with ID {request.ActionId} is already assigned to the Menu with ID {request.MenuId}.", System.Net.HttpStatusCode.Conflict);
            }
            await menuActionRepository.AddAsync(new Domain.Entity.RBAC.MenuAction
            {
                MenuId = request.MenuId,
                ActionId = request.ActionId
            });
            return Success(true, "MenuAction added successfully.");
        }
    }

}