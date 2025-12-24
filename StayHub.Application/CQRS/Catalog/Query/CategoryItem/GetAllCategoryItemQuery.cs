using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.CategoryItem
{
    public record GetAllCategoryItemQuery(string? search = null, int? pageNumber = null, int? pageSize = null) : IRequest<Response<CategoryItemDTO>>;

    public class GetAllCategoryItemQueryHandler(ICategoryItemRepository categoryItemRepository, IConfiguration _configuration)
        : BaseResponseHandler, IRequestHandler<GetAllCategoryItemQuery, Response<CategoryItemDTO>>
    {
        public async Task<Response<CategoryItemDTO>> Handle(GetAllCategoryItemQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.pageSize ?? _configuration.GetValue<int>("PageSize");
            var (items, totalCount) = await categoryItemRepository.GetManyPagedAsync(
                pageNumber: request.pageNumber ?? 1,
                pageSize: pageSize,
                filter: e => string.IsNullOrEmpty(request.search) ? true : e.Name.Contains(request.search ?? "") || e.Name == request.search,
                selector: (entity, index) => new CategoryItemDTO
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Code = entity.Code,
                    Value = entity.Value,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                });

            return SuccessPaginated<CategoryItemDTO>(data: items, totalCount: totalCount, currentPage: request.pageNumber ?? 1, pageSize: pageSize);
        }
    }
}

