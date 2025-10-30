using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.User
{
    // Include properties to be used as input for the query
    public record GetProfileQuery(int Id) : IRequest<BaseResponse<ProfileDTO>>;
    public sealed class GetProfileQueryHandler(IUserRepository userRepository) : BaseResponseHandler, IRequestHandler<GetProfileQuery, BaseResponse<ProfileDTO>>
    {
        public async Task<BaseResponse<ProfileDTO>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await userRepository.FindOneAsync(e => e.Id == request.Id, selector: e => new ProfileDTO
            {
                Id = e.Id,
                Username = e.Username,
                Email = e.Email,
                Roles = e.UserRoles.Select(ur => new RoleDTO
                {
                    Id = ur.Role.Id,
                    Name = ur.Role.Name,
                    Code = ur.Role.Code,
                    Description = ur.Role.Description
                }).ToList()
            });
            if (profile != null)
            {
                return Success<ProfileDTO>(profile);
            }
            return Failure<ProfileDTO>("User not found", System.Net.HttpStatusCode.NotFound);
        }
    }

}
