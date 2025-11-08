using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;


namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    // TODO  pagination
    public record GetAllMenuQuery() : IRequest<BaseResponse<List<MenuDTO>>>;
    public sealed class GetAllMenuQueryHandler(IMenuRepository menuRepository) : BaseResponseHandler, IRequestHandler<GetAllMenuQuery, BaseResponse<List<MenuDTO>>>
    {
        public async Task<BaseResponse<List<MenuDTO>>> Handle(GetAllMenuQuery request, CancellationToken cancellationToken)
        {
            var result = await menuRepository.GetAllAsync(selector: (e, i) => new MenuDTO
            {
                Id = e.Id,
                Path = e.Path,
                Name = e.Name,
                Description = e.Description,
                Icon = e.Icon,
                GroupId = e.MenuGroupId,
                ParentId = e.ParentId,
                IsActive = e.IsActive,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            });
            Dictionary<int, MenuDTO> menuMap = result.ToDictionary(e => e.Id, e => e);
            var menuGroup = new List<MenuDTO>();
            foreach (var menu in menuMap)
            {
                int? parentId = menu.Value.ParentId;
                if (parentId != null && menuMap.ContainsKey(parentId.Value))
                {
                    var parentMenu = menuMap[(int)parentId];
                    if(parentMenu.Children == null)
                    {
                        parentMenu.Children = new List<MenuDTO>();
                    }
                    parentMenu.Children.Add(menu.Value);
                }
                else
                {
                    menuGroup.Add(menu.Value);
                }
            }
            return Success(menuGroup);
        }
    }

}
