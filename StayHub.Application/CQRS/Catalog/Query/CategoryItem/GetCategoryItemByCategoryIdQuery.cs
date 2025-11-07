using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.CategoryItem
{
    // Query record
    public record GetCategoryItemByCategoryIdQuery(int CategoryId)
        : IRequest<BaseResponse<List<CategoryItemDTO>>>;

    // Handler
    public sealed class GetCategoryItemByCategoryIdQueryHandler(ICategoryItemRepository categoryItemRepository)
        : BaseResponseHandler, IRequestHandler<GetCategoryItemByCategoryIdQuery, BaseResponse<List<CategoryItemDTO>>>
    {
        public async Task<BaseResponse<List<CategoryItemDTO>>> Handle(GetCategoryItemByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var items = await categoryItemRepository.GetManyAsync<CategoryItemDTO>(
                filter: e => e.CategoryId == request.CategoryId,
                selector: (e, i) => new CategoryItemDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Code = e.Code,
                    Description = e.Value,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                });

            if (items == null || !items.Any())
                return Failure<List<CategoryItemDTO>>("No category items found for the specified CategoryId", System.Net.HttpStatusCode.BadRequest);

            return Success(items.ToList());
        }
    }
}

