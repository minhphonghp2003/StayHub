using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Services;

namespace StayHub.Application.CQRS.RBAC.Command.Token
{
    // Include properties to be used as input for the command
    public record RefreshTokenCommand(string Token) : IRequest<BaseResponse<TokenDTO>>;
    public sealed class RefreshTokenCommandHandler(ITokenRepository tokenRepository, IJwtService jwtService) : BaseResponseHandler, IRequestHandler<RefreshTokenCommand, BaseResponse<TokenDTO>>
    {
        public async Task<BaseResponse<TokenDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await tokenRepository.GetTokenInfo(request.Token);

            if (token == null)
            {
                return Failure<TokenDTO>("Invalid refresh token", System.Net.HttpStatusCode.Unauthorized);
            }
            if (token.RevokedAt != null)
            {
                return Failure<TokenDTO>("Refresh token has been revoked", System.Net.HttpStatusCode.Unauthorized);
            }
            if (token.ExpireDate < DateTime.UtcNow)
            {
                return Failure<TokenDTO>("Refresh token has expired", System.Net.HttpStatusCode.Unauthorized);
            }

            var newToken = await jwtService.GenerateRefreshToken();
            var accessToken = await jwtService.GenerateJwtToken(token.User, new List<string>());
            return Success<TokenDTO>(new TokenDTO
            {
                Username = token.User.Username,
                Id = token.User.Id,
                Token = accessToken.Item1,
                ExpiresDate = accessToken.Item2,
                RefreshToken = newToken,
            });
        }
    }

}