using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.Category
{
    public record GetAllCategoryQuery() : IRequest<BaseResponse<List<CategoryDTO>>>;

    public class GetAllCategoryQueryHandler(ICategoryRepository categoryRepository)
        : BaseResponseHandler, IRequestHandler<GetAllCategoryQuery, BaseResponse<List<CategoryDTO>>>
    {
        public async Task<BaseResponse<List<CategoryDTO>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoryRepository.GetAllAsync(
                (entity, index) => new CategoryDTO
                {
                    Id = entity.Id,
                    Name = entity.Name ?? string.Empty,
                    Code = entity.Code,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                });

            return Success(categories.ToList());
        }
    }
}

