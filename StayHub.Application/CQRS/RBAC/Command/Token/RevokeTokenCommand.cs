using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.Token
{
    // Include properties to be used as input for the command
    public record RevokeTokenCommand(string refreshToken) : IRequest<BaseResponse<bool>>;
    internal sealed class RevokeTokenCommandHandler(ITokenRepository tokenRepository) : BaseResponseHandler, IRequestHandler<RevokeTokenCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var token =await tokenRepository.GetTokenInfo(request.refreshToken);
            if (token == null)
            {
                return Failure<bool>("Invalid refresh token", System.Net.HttpStatusCode.BadRequest);
            }
            token.RevokedAt = DateTime.UtcNow;
            tokenRepository.Update(token);
            return Success<bool>(data: true);
        }
    }

}