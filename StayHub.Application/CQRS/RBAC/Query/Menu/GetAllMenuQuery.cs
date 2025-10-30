using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    // Include properties to be used as input for the query
    public record GetAllMenuQuery() : IRequest<BaseResponse<List<MenuDTO>>>;
    public sealed class GetAllMenuQueryHandler(IMenuRepository menuRepository) :BaseResponseHandler, IRequestHandler<GetAllMenuQuery, BaseResponse<List<MenuDTO>>>
    {
        public async Task<BaseResponse<List<MenuDTO>>> Handle(GetAllMenuQuery request, CancellationToken cancellationToken)
        {
            var result = await menuRepository.GetAllAsync(selector:(e,i)=>new MenuDTO
            {
                Id = e.Id,
                Path = e.Path,
                Description = e.Description,
                Icon = e.Icon,
                ParentId = e.ParentId,
                IsActive = e.IsActive,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            });
            return Success<List<MenuDTO>>(data:result.ToList());
        }
    }

}
