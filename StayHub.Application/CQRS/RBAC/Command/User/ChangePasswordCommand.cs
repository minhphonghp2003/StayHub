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
    public record ChangePasswordCommand(string oldPassword, string newPassword) : IRequest<BaseResponse<bool>>;
    public sealed class ChangePasswordCommandHandler(IUserRepository userRepository,HttpContext context ) : BaseResponseHandler, IRequestHandler<ChangePasswordCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = context.GetUserId();
            if (userId == null) return Failure<bool>("User not found", HttpStatusCode.NotFound);
            var user = await userRepository.GetEntityByIdAsync(userId!.Value);

            if (user == null)
            {
                return Failure<bool>("No user found", HttpStatusCode.BadRequest);
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.oldPassword, user.Password);
            if (!isPasswordValid)
            {
                return Failure<bool>("Old password is incorrect", HttpStatusCode.BadRequest);
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.newPassword);
            user.Password = hashedPassword;
            userRepository.Update(user);
            return Success<bool>(true, "Password changed successfully");
        }
    }
}
