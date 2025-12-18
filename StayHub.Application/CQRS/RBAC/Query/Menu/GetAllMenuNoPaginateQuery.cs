using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    public record GetAllMenuNoPaginateQuery() : IRequest<Response<List<CategoryItemDTO>>>;
    public sealed class GetAllMenuNoPaginateQueryHandler(IMenuRepository menuRepository, IConfiguration _configuration) : BaseResponseHandler, IRequestHandler<GetAllMenuNoPaginateQuery, Response<List<CategoryItemDTO>>>
    {
        public async Task<Response<List<CategoryItemDTO>>> Handle(GetAllMenuNoPaginateQuery request, CancellationToken cancellationToken)
        {
            var items = await menuRepository.GetAllAsync(selector:(item,index)=>new CategoryItemDTO
            {
                Id = item.Id,
                Name = item.Name,
            });
            return Success<List<CategoryItemDTO>>(data: items.ToList());
        }
    }
}
