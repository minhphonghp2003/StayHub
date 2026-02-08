using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shared.Services;
using StayHub.Application.CQRS.Common.Command.Email;

namespace StayHub.Application.CQRS.RBAC.Command.User
{
    public record NewPasswordCommand(string password, string token) : IRequest<BaseResponse<bool>>;
    public sealed class NewPasswordCommandHandler(IMediator mediator,IUserRepository userRepository,HashService hashService, Base64Service base64Service,IConfiguration configuration ) : BaseResponseHandler, IRequestHandler<NewPasswordCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(NewPasswordCommand request, CancellationToken cancellationToken)
        {
            var decodedToken = base64Service.Decode(request.token);
            var tokenParts = decodedToken.Split('|');
            var email = tokenParts[0];
            var expireBinary = long.Parse(tokenParts[1]);
            var expireTime = DateTime.FromBinary(expireBinary);
            if (DateTime.UtcNow.ToLocalTime() > expireTime)
            {
                return Failure<bool>(message:"Token has expired.", code: HttpStatusCode.BadRequest);
            }
            var user = await userRepository.FindOneEntityAsync(filter:e=>e.Profile.Email==email && e.ResetPasswordToken==request.token);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.password);
            if (user != null)
            {
                user.Password = hashedPassword;
                user.ResetPasswordToken = null;
                userRepository.Update(user);
            }
            return Success<bool>(true, "Password has been reset successfully.");
        }
    }
}
