
using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.Token
{
    // Include properties to be used as input for the command
    public record RevokeAllUserTokenCommand(int userId) : IRequest<BaseResponse<bool>>;

    internal sealed class RevokeAllUserTokenCommandHandler(ITokenRepository tokenRepository)
        : BaseResponseHandler, IRequestHandler<RevokeAllUserTokenCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(RevokeAllUserTokenCommand request,
            CancellationToken cancellationToken)
        {
            var tokens = await tokenRepository.GetManyAsync(
                filter: t => t.UserId == request.userId,
                selector: ((token, i) => token)
            );

            if (!tokens.Any())
                return Success<bool>(data: true);

            var revokedAt = DateTime.UtcNow;

            foreach (var token in tokens)
            {
                token.RevokedAt = revokedAt;
            }

            await tokenRepository.SaveAsync();

            return Success<bool>(data: true);
        }
    }
}