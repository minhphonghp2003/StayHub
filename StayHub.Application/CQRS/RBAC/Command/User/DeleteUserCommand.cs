using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StayHub.Application.Extension;

namespace StayHub.Application.CQRS.RBAC.Command.User
{
    public record DeleteUserCommand(int Id) : IRequest<BaseResponse<bool>>;

    public sealed class DeleteUserCommandHandler(IUserRepository userRepository, IHttpContextAccessor accessor)
        : BaseResponseHandler, IRequestHandler<DeleteUserCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = accessor.HttpContext.GetUserId() ?? 0;
            var isSystemUser = await userRepository.IsSystemUser(currentUserId);
            if (currentUserId == request.Id)
            {
                return Failure<bool>("You cannot delete your own account", HttpStatusCode.BadRequest);
            }

            if (isSystemUser)
            {
                await userRepository.Delete(new Domain.Entity.RBAC.User { Id = request.Id });
            }
            else
            {
                await userRepository.DeleteWhere(e => e.Id == request.Id && e.CreatedByUserId == currentUserId);
            }

            return Success<bool>(true);
        }
    }
}