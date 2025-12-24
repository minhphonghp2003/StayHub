using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.CategoryItem
{
    // Query record
    public record GetCategoryItemByCategoryCodeQuery(string CategoryCode)
        : IRequest<BaseResponse<List<CategoryItemDTO>>>;

    // Handler
    public sealed class GetCategoryItemByCategoryCodeQueryHandler(ICategoryItemRepository categoryItemRepository)
        : BaseResponseHandler, IRequestHandler<GetCategoryItemByCategoryCodeQuery, BaseResponse<List<CategoryItemDTO>>>
    {
        public async Task<BaseResponse<List<CategoryItemDTO>>> Handle(GetCategoryItemByCategoryCodeQuery request, CancellationToken cancellationToken)
        {
            var items = await categoryItemRepository.GetManyAsync(
                filter: e => e.Category != null && e.Category.Code == request.CategoryCode,
                selector: (e,i) => new CategoryItemDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Code = e.Code,
                    Value = e.Value,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                });

            if (items == null || !items.Any())
                return Failure<List<CategoryItemDTO>>(
                    "No category items found for the specified CategoryCode",
                    System.Net.HttpStatusCode.BadRequest
                );

            return Success(items.ToList());
        }
    }
}

