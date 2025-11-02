using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.Role
{
    public record DeleteRoleCommand(int Id) : IRequest<BaseResponse<bool>>;
    public sealed class DeleteRoleCommandHandler(IRoleRepository roleRepository) : BaseResponseHandler, IRequestHandler<DeleteRoleCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            await roleRepository.Delete(new Domain.Entity.RBAC.Role { Id = request.Id });
            return Success(true);
        }
    }
}
