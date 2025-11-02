using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.User
{
    public record DeleteUserCommand(int Id) : IRequest<BaseResponse<bool>>;
    public sealed class DeleteUserCommandHandler(IUserRepository userRepository) : BaseResponseHandler, IRequestHandler<DeleteUserCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await userRepository.Delete(new Domain.Entity.RBAC.User { Id = request.Id });
            return Success<bool>(true);
        }
    }
}
