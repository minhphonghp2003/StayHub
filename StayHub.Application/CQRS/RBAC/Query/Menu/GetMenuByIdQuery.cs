using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    public record GetMenuByIdQuery(int Id) : IRequest<BaseResponse<MenuDTO>>;
    internal sealed class GetMenuByIdQueryHandler(IMenuRepository menuRepository) : BaseResponseHandler, IRequestHandler<GetMenuByIdQuery, BaseResponse<MenuDTO>>
    {
        public async Task<BaseResponse<MenuDTO>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await menuRepository.FindOneAsync(filter: e => e.Id == request.Id, selector: (e) => new MenuDTO
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
            if (result == null)
            {
                return Failure<MenuDTO>(message: "Menu not found", code: System.Net.HttpStatusCode.BadRequest);
            }
            return Success<MenuDTO>(result);
        }
    }
}
