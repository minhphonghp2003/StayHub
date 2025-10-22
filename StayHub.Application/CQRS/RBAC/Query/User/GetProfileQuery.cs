using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.User
{
    // Include properties to be used as input for the query
    public record GetProfileQuery() : IRequest<BaseResponse<ProfileDTO>>;
    public sealed class GetProfileQueryHandler :BaseResponseHandler, IRequestHandler<GetProfileQuery, BaseResponse<ProfileDTO>>
    {
        public async Task<BaseResponse<ProfileDTO>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            return Failure<ProfileDTO>("Not Implemented", System.Net.HttpStatusCode.NotImplemented);
        }
    }

}
