using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int ParentId { get; set; }

        public UpdateMenuCommand() { }

        public UpdateMenuCommand(int id, string path, string name, string description, string icon, int parentId)
        {
            Id = id;
            Path = path;
            Name = name;
            Description = description;
            Icon = icon;
            ParentId = parentId;
        }
    }
    public sealed class UpdateMenuCommandCommandHandler(IMenuRepository menuRepository) : BaseResponseHandler, IRequestHandler<UpdateMenuCommand, BaseResponse<MenuDTO>>
    {
        public async Task<BaseResponse<MenuDTO>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = new StayHub.Domain.Entity.RBAC.Menu
            {
                Id = request.Id,
                Path = request.Path,
                Name = request.Name,
                Description = request.Description,
                Icon = request.Icon,
                ParentId = request.ParentId,

            };
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
                CreatedAt = menu.CreatedAt,
                UpdatedAt = menu.UpdatedAt
            });

        }
    }
}
