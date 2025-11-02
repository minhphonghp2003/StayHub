using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.RoleAction
{
    public record DeleteRoleActionCommand(int RoleId, int ActionId) : IRequest<BaseResponse<bool>>;
    public sealed class DeleteRoleActionCommandHandler(IRoleActionRepository roleActionRepository) : BaseResponseHandler, IRequestHandler<DeleteRoleActionCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(DeleteRoleActionCommand request, CancellationToken cancellationToken)
        {
            await roleActionRepository.Delete(new Domain.Entity.RBAC.RoleAction { RoleId = request.RoleId, ActionId = request.ActionId });
            return Success<bool>(true);
        }
    }
}
