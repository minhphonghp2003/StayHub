using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.User
{
    public record GetAllUserNoPagingQuery(string searchKey) : IRequest<BaseResponse<List<UserDTO>>>;

    public sealed class GetAllUserNoPagingQueryHandler(
        IUserRepository userRepository,
        IConfiguration configuration,
        IHttpContextAccessor contextAccessor)
        : BaseResponseHandler, IRequestHandler<GetAllUserNoPagingQuery, BaseResponse<List<UserDTO>>>
    {
        public async Task<BaseResponse<List<UserDTO>>> Handle(GetAllUserNoPagingQuery request,
            CancellationToken cancellationToken)
        {
            var currentUserId = contextAccessor.HttpContext.GetUserId();
            if (request.searchKey.Length <= 1)
            {
                return Success(new List<UserDTO>());
            }

            var items = await userRepository.GetManyAsync(filter: e =>
                    e.Id != currentUserId &&
                    e.IsActive == true &&
                    e.CreatedByUserId == currentUserId &&
                    (e.Username.ToLower().Contains(request.searchKey.ToLower()) ||
                     e.Profile.Email.ToLower().Contains(request.searchKey.ToLower()) ||
                     e.Profile.Phone.ToLower().Contains(request.searchKey.ToLower()) ||
                     e.Profile.Fullname.ToLower().Contains(request.searchKey.ToLower())),
                include: e => e.Include(j => j.Profile), selector: (e, i) => new UserDTO
                {
                    Id = e.Id,
                    Username = e.Username,
                    Email = e.Profile.Email,
                    Phone = e.Profile.Phone,
                    Fullname = e.Profile.Fullname
                });
            return Success<List<UserDTO>>(items.ToList());
        }
    }
}