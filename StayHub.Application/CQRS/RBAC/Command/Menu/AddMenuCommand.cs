using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.Menu
{
    // Include properties to be used as input for the command
    public record AddMenuCommand(string Name, string Path,int GroupId, string? Description, string? Icon, int? ParentId) : IRequest<BaseResponse<MenuDTO>>;
    public sealed class AddMenuCommandHandler(IMenuRepository menuRepository) : BaseResponseHandler, IRequestHandler<AddMenuCommand, BaseResponse<MenuDTO>>
    {
        public async Task<BaseResponse<MenuDTO>> Handle(AddMenuCommand request, CancellationToken cancellationToken)
        {
            if (await menuRepository.ExistsAsync(e => e.Path == request.Path))
            {
                return Failure<MenuDTO>(message: $"Menu with path '{request.Path}' already exists.", code: System.Net.HttpStatusCode.BadRequest);
            }
            if (request.ParentId != null && !await menuRepository.ExistsAsync((int)request.ParentId))
            {

                return Failure<MenuDTO>(message: $"Parent menu not found",code:System.Net.HttpStatusCode.BadRequest);
            }
            var menu = new StayHub.Domain.Entity.RBAC.Menu
            {
                Name = request.Name,
                Path = request.Path,
                Description = request.Description,
                Icon = request.Icon,
                ParentId = request.ParentId,
                MenuGroupId = request.GroupId,
                IsActive = true
                
            };
            await menuRepository.AddAsync(menu);
            return Success<MenuDTO>(new MenuDTO
            {
                Id = menu.Id,
                Name = request.Name,
                Path = request.Path,
                Description = request.Description,
                Icon = request.Icon,
                ParentId = request.ParentId,
                IsActive = true
            });
        }
    }

}