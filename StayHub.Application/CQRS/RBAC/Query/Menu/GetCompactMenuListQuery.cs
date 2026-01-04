using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    // Include properties to be used as input for the query
    public record GetCompactMenuListQuery(string? search = null, int? pageNumber = null, int? pageSize = null) : IRequest<Response<MenuDTO>>;
    public sealed class GetCompactMenuListQueryHandler(IMenuRepository menuRepository, IConfiguration _configuration) : BaseResponseHandler, IRequestHandler<GetCompactMenuListQuery, Response<MenuDTO>>
    {
        public async Task<Response<MenuDTO>> Handle(GetCompactMenuListQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.pageSize ?? _configuration.GetValue<int>("PageSize");
            var (items, count) = await menuRepository.GetManyPagedAsync(pageNumber: request.pageNumber ?? 1, pageSize: pageSize,
                    filter: e => string.IsNullOrEmpty(request.search) ? true : e.Name.Contains(request.search ?? "") || e.Name == request.search,
                    selector: (e, i) => new MenuDTO
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Path = e.Path,
                    }

                );
            return SuccessPaginated<MenuDTO>(data: items, totalCount: count, pageSize: pageSize, currentPage: request.pageNumber ?? 1);
        }
    }

}
