using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.Menu
{
    public class UpdateMenuCommand : IRequest<BaseResponse<MenuDTO>>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Path { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Icon { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int GroupId { get; set; }
        public int? Order { get; set; }

        public UpdateMenuCommand() { }

        public UpdateMenuCommand(int id, string path, int groupId, string name, string? description, string? icon, int? parentId, int? order)
        {
            Id = id;
            Path = path;
            Name = name;
            Description = description;
            Icon = icon;
            ParentId = parentId;
            GroupId = groupId;
            Order = order;
        }
    }
    public sealed class UpdateMenuCommandCommandHandler(IMenuRepository menuRepository) : BaseResponseHandler, IRequestHandler<UpdateMenuCommand, BaseResponse<MenuDTO>>
    {
        public async Task<BaseResponse<MenuDTO>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await menuRepository.FindOneEntityAsync(filter: e => e.Id == request.Id);
            if (menu == null)
            {
                return Failure<MenuDTO>(message: "No menu found", code: HttpStatusCode.BadRequest);

            }
            menu.Path = request.Path;
            menu.Name = request.Name;
            menu.Description = request.Description;
            menu.Icon = request.Icon;
            menu.ParentId = request.ParentId;
            menu.MenuGroupId = request.GroupId;
            menu.Order = request.Order ?? menu.Order;
            menuRepository.Update(menu);
            return Success<MenuDTO>(new MenuDTO
            {
                Id = menu.Id,
                Path = menu.Path,
                Name = menu.Name,
                Description = menu.Description,
                Icon = menu.Icon,
                ParentId = menu.ParentId,
                IsActive = menu.IsActive,
                Order = menu.Order,
                CreatedAt = menu.CreatedAt,
                UpdatedAt = menu.UpdatedAt
            });

        }
    }
}
