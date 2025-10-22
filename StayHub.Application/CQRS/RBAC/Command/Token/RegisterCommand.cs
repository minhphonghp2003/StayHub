using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.Token
{
    // Include properties to be used as input for the command
    public record RegisterCommand(String Username, String Password,String Email) : IRequest<BaseResponse<TokenDTO>>;
    public sealed class RegisterCommandHandler :BaseResponseHandler, IRequestHandler<RegisterCommand, BaseResponse<TokenDTO>>
    {
        public async Task<BaseResponse<TokenDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return Failure<TokenDTO>("Not Implemented", System.Net.HttpStatusCode.NotImplemented);
        }
    }

}