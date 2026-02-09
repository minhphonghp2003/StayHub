using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shared.Services;
using StayHub.Application.CQRS.Common.Command.Email;

namespace StayHub.Application.CQRS.RBAC.Command.User
{
    public record ForgetPasswordCommand(string email) : IRequest<BaseResponse<int>>;
    public sealed class ForgetPasswordCommandHandler(IMediator mediator,IUserRepository userRepository, Base64Service base64Service,IConfiguration configuration ) : BaseResponseHandler, IRequestHandler<ForgetPasswordCommand, BaseResponse<int>>
    {
        public async Task<BaseResponse<int>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var expireMinutes = configuration.GetValue<int>("ResetPasswordTokenExpireMinutes");
            var now = DateTime.UtcNow.ToLocalTime();
            var expireTime = now.AddMinutes(expireMinutes);
            var tokenPayload = $"{request.email}|{expireTime.ToBinary()}";
            var token = base64Service.Encode(tokenPayload);
            var user = await userRepository.FindOneEntityAsync(filter:e=>e.Profile.Email==request.email);
            if (user != null)
            {
                user.ResetPasswordToken = token;
                userRepository.Update(user);
            }
             mediator.Publish(new SendForgetPasswordEmailEvent(
                request.email,token,expireMinutes));
            return Success<int>(expireMinutes);
        }
    }
}
