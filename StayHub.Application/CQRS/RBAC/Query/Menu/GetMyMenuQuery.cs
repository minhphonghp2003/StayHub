using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    public record GetMyMenuQuery(int? propertyId) : IRequest<BaseResponse<List<MenuGroupDTO>>>;
    public sealed class GetMenuOfUserQueryHandler(IHttpContextAccessor httpContextAccessor,IUserRepository userRepository, IMenuRepository menuRepository) : BaseResponseHandler, IRequestHandler<GetMyMenuQuery, BaseResponse<List<MenuGroupDTO>>>
    {
        public async Task<BaseResponse<List<MenuGroupDTO>>> Handle(GetMyMenuQuery request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext?.GetUserId();
            return Success(await menuRepository.GetUserMenu(await userRepository.IsSystemUser(userId??0), userId??0, request.propertyId));
        }
    }

}
