using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    // Include properties to be used as input for the query
    public record GetMyMenuQuery() : IRequest<BaseResponse<List<MenuDTO>>>;
    public sealed class GetMenuOfUserQueryHandler(IHttpContextAccessor httpContextAccessor, IMenuRepository menuRepository) : BaseResponseHandler, IRequestHandler<GetMyMenuQuery, BaseResponse<List<MenuDTO>>>
    {
        public async Task<BaseResponse<List<MenuDTO>>> Handle(GetMyMenuQuery request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.GetUserId();
            if (userId == null)
            {
                return Failure<List<MenuDTO>>(message: "Unauthorized", code: System.Net.HttpStatusCode.Unauthorized);
            }
            return Success(await menuRepository.GetUserMenu((int)userId));
        }
    }

}
