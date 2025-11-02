using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.MenuAction
{
    public record DeleteMenuActionCommand(int MenuId, int ActionId) : IRequest<BaseResponse<bool>>;
    public sealed class DeleteMenuActionCommandHandler(IMenuActionRepository menuActionRepository) : BaseResponseHandler, IRequestHandler<DeleteMenuActionCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(DeleteMenuActionCommand request, CancellationToken cancellationToken)
        {
            await menuActionRepository.Delete(new Domain.Entity.RBAC.MenuAction { MenuId = request.MenuId, ActionId = request.ActionId });
            return Success<bool>(true);
        }
    }
}
