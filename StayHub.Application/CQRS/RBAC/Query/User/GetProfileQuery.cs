using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.User
{
    // Include properties to be used as input for the query
    public record GetProfileQuery(int Id) : IRequest<BaseResponse<ProfileDTO>>;
    public sealed class GetProfileQueryHandler(IUserRepository userRepository) :BaseResponseHandler, IRequestHandler<GetProfileQuery, BaseResponse<ProfileDTO>>
    {
        public async Task<BaseResponse<ProfileDTO>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await userRepository.FindOneAsync(e=>e.Id==request.Id);
            if (profile != null)
            {
                return Success<ProfileDTO>(new ProfileDTO
                {
                    Id = profile.Id,
                    Username = profile.Username,
                    Email = profile.Email ?? string.Empty,
                });
            }
            return Failure<ProfileDTO>("User not found", System.Net.HttpStatusCode.NotFound);
        }
    }

}
