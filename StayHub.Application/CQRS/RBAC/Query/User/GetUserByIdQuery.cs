using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.User
{
    // Include properties to be used as input for the query
    public record GetUserByIdQuery(int Id) : IRequest<BaseResponse<ProfileDTO>>;
    public sealed class GetProfileQueryHandler(IUserRepository userRepository) : BaseResponseHandler, IRequestHandler<GetUserByIdQuery, BaseResponse<ProfileDTO>>
    {
        public async Task<BaseResponse<ProfileDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var profile = await userRepository.GetProfile(request.Id);
            return Success<ProfileDTO>(profile);
        }
    }

}
