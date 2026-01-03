using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.User
{
    public record SetActivateUserCommand(int Id, bool Activated) : IRequest<BaseResponse<bool>>;
    public sealed class SetActivateUserCommandHandler(IUserRepository UserRepository) : BaseResponseHandler, IRequestHandler<SetActivateUserCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(SetActivateUserCommand request, CancellationToken cancellationToken)
        {
            return Success<bool>(await UserRepository.SetActivated(request.Id, request.Activated));

        }
    }
}
