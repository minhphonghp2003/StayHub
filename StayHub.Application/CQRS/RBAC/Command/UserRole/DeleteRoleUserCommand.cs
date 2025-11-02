using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.UserRole
{
    public record DeleteUserRoleCommand(int RoleId, int UserId) : IRequest<BaseResponse<bool>>;
    public sealed class DeleteUserRoleCommandHandler(IUserRoleRepository userRoleRepository) : BaseResponseHandler, IRequestHandler<DeleteUserRoleCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            await userRoleRepository.Delete(new Domain.Entity.RBAC.UserRole { UserId = request.UserId, RoleId = request.RoleId });
            return Success<bool>(true);
        }
    }
}
