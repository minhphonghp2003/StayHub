using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    // Include properties to be used as input for the query
    public record GetMyMenuQuery() : IRequest<BaseResponse<List<MenuGroupDTO>>>;
    public sealed class GetMenuOfUserQueryHandler(IHttpContextAccessor httpContextAccessor, IMenuRepository menuRepository) : BaseResponseHandler, IRequestHandler<GetMyMenuQuery, BaseResponse<List<MenuGroupDTO>>>
    {
        public async Task<BaseResponse<List<MenuGroupDTO>>> Handle(GetMyMenuQuery request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.GetUserId();
            if (userId == null)
            {
                return Failure<List<MenuGroupDTO>>(message: "Unauthorized", code: System.Net.HttpStatusCode.Unauthorized);
            }
            return Success(await menuRepository.GetUserMenu((int)userId));
        }
    }

}
