using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.CategoryItem
{
    // Query record
    public record GetCategoryItemByCategoryIdQuery(int categoryId, int? pageNumber, int? pageSize, string? search)
        : IRequest<Response<CategoryItemDTO>>;

    // Handler
    public sealed class GetCategoryItemByCategoryIdQueryHandler(ICategoryItemRepository categoryItemRepository, IConfiguration configuration)
        : BaseResponseHandler, IRequestHandler<GetCategoryItemByCategoryIdQuery, Response<CategoryItemDTO>>
    {
        public async Task<Response<CategoryItemDTO>> Handle(GetCategoryItemByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.pageSize ?? configuration.GetValue<int>("pageSize");

            var (items, count) = await categoryItemRepository.GetManyPagedAsync<CategoryItemDTO>(
                pageNumber: request.pageNumber ?? 1,
                pageSize: pageSize,
                filter: e => (request.categoryId == 0 || e.CategoryId == request.categoryId) && (string.IsNullOrEmpty(request.search) || e.Code.Contains(request.search) || e.Code.Contains(request.search) || e.Value.Contains(request.search)),
                selector: (e, i) => new CategoryItemDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Code = e.Code,
                    Value = e.Value,
                    Description = e.Value,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                });

            if (items == null || !items.Any())
                return FailurePaginated<CategoryItemDTO>(message: "No category items found for the specified CategoryId", code: System.Net.HttpStatusCode.BadRequest);

            return SuccessPaginated<CategoryItemDTO>(items.ToList(), count, pageSize, request.pageNumber ?? 1);
        }
    }
}

