using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.CategoryItem
{
    public record GetAllCategoryItemQuery() : IRequest<BaseResponse<List<CategoryItemDTO>>>;

    public class GetAllCategoryItemQueryHandler(ICategoryItemRepository categoryItemRepository)
        : BaseResponseHandler, IRequestHandler<GetAllCategoryItemQuery, BaseResponse<List<CategoryItemDTO>>>
    {
        public async Task<BaseResponse<List<CategoryItemDTO>>> Handle(GetAllCategoryItemQuery request, CancellationToken cancellationToken)
        {
            var items = await categoryItemRepository.GetAllAsync(
                (entity, index) => new CategoryItemDTO
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Code = entity.Code,
                    Description = entity.Value,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                });

            return Success(items.ToList());
        }
    }
}

