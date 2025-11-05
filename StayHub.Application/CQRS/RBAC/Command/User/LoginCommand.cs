using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Services;
using System.Net;

namespace StayHub.Application.CQRS.RBAC.Command.Token
{
    // Include properties to be used as input for the command
    public record LoginCommand(string Username, string Password) : IRequest<BaseResponse<TokenDTO>>;
    public sealed class LoginCommandHandler(IUserRepository userRepository, IJwtService tokenService, ITokenRepository tokenRepository, IAuthService authService) : BaseResponseHandler, IRequestHandler<LoginCommand, BaseResponse<TokenDTO>>
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

                return Failure<TokenDTO>("Invalid username or password", HttpStatusCode.BadRequest);
            }
            var token = await tokenService.GenerateJwtToken(user, new List<string>());
            var (refreshToken, expires) = await authService.GenerateRefreshToken(userId: user.Id);
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