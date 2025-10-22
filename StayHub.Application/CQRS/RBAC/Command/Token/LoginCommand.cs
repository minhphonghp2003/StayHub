using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using System.Net;

namespace StayHub.Application.CQRS.RBAC.Command.Token
{
    // Include properties to be used as input for the command
    public record LoginCommand(string Username,string Password) : IRequest<BaseResponse<TokenDTO>>;
    public sealed class LoginCommandHandler :BaseResponseHandler, IRequestHandler<LoginCommand, BaseResponse<TokenDTO>>
    {
        public async Task<BaseResponse<TokenDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return Failure<TokenDTO>("Not Found", HttpStatusCode.NotFound);
        }
    }

}