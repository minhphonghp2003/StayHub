using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Services;
using System.Net;
using Microsoft.AspNetCore.Http;
using StayHub.Application.CQRS.Common.Command.Email;
using StayHub.Application.Extension;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.Token
{
    // Include properties to be used as input for the command
    public record LoginCommand(string Username, string Password) : IRequest<BaseResponse<TokenDTO>>;
    public sealed class LoginCommandHandler(IUserRepository userRepository, IJwtService tokenService, IMediator mediatR, IAuthService authService,IHttpContextAccessor httpContextAccessor) : BaseResponseHandler, IRequestHandler<LoginCommand, BaseResponse<TokenDTO>>
    {
        public async Task<BaseResponse<TokenDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindOneEntityAsync(e => e.Username == request.Username, include: e => e.Include(u => u.Profile));
            if (user == null)
            {
            
                return Failure<TokenDTO>("Invalid username or password", HttpStatusCode.BadRequest);
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!isPasswordValid)
            {
              await  mediatR.Publish(new UpdateLoginActivityEvent(new LoginActivity
                {
                    UserId = user.Id,
                    Time = DateTime.UtcNow,
                    Status =false,
                    IP = httpContextAccessor.HttpContext?.GetIp(),
                    Browser = httpContextAccessor.HttpContext?.GetBrowser(),
                    OS = httpContextAccessor.HttpContext?.GetOS()
                }));
                return Failure<TokenDTO>("Invalid username or password", HttpStatusCode.BadRequest);
            }
            var token = await tokenService.GenerateJwtToken(user, new List<string>());
            var (refreshToken, expires) = await authService.GenerateRefreshToken(userId: user.Id);
            await mediatR.Publish(new UpdateLoginActivityEvent(new LoginActivity
            {
                UserId = user.Id,
                Time = DateTime.UtcNow,
                Status = true,
                IP = httpContextAccessor.HttpContext?.GetIp(),
                Browser = httpContextAccessor.HttpContext?.GetBrowser(),
                OS = httpContextAccessor.HttpContext?.GetOS()
            }));
            return Success<TokenDTO>(new TokenDTO
            {
                Email = user?.Profile?.Email,
                Fullname = user?.Profile?.Fullname,
                Image = user?.Profile?.Image,
                Id = user.Id,
                Token = token.Item1,
                ExpiresDate = expires,
                RefreshToken = refreshToken
            }, "Login successfully");

        }
    }

}