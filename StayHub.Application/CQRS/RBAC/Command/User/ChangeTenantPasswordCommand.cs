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
using Microsoft.Extensions.Configuration;
using Shared.Services;
using StayHub.Application.CQRS.Common.Command.Email;
using StayHub.Application.Extension;

namespace StayHub.Application.CQRS.RBAC.Command.User
{
    public record ChangeTenantPasswordCommand(int userId, string newPassword) : IRequest<BaseResponse<bool>>;
    public sealed class ChangeTenantPasswordCommandHandler(IUserRepository userRepository,HttpContext context ) : BaseResponseHandler, IRequestHandler<ChangeTenantPasswordCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(ChangeTenantPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetEntityByIdAsync(request.userId);

            if (user == null)
            {
                return Failure<bool>("No user found", HttpStatusCode.BadRequest);
            }
           
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.newPassword);
            user.Password = hashedPassword;
            userRepository.Update(user);
            return Success<bool>(true, "Password changed successfully");
        }
    }
}