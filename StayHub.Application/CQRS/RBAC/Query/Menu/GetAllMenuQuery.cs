using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;


namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    public record GetAllMenuQuery(string? search = null, int? pageNumber = null, int? pageSize = null) : IRequest<Response<MenuDTO>>;
    public sealed class GetAllMenuQueryHandler(IMenuRepository menuRepository, IConfiguration _configuration) : BaseResponseHandler, IRequestHandler<GetAllMenuQuery, Response<MenuDTO>>
    {
        public async Task<Response<MenuDTO>> Handle(GetAllMenuQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.pageSize ?? _configuration.GetValue<int>("PageSize");
            var (items, count) = await menuRepository.GetAllPaginatedMenu(request.pageNumber ?? 1, pageSize, request.search);
            return SuccessPaginated<MenuDTO>(data: items, totalCount: count, pageSize: pageSize, currentPage: request.pageNumber ?? 1);
        }
    }

}
