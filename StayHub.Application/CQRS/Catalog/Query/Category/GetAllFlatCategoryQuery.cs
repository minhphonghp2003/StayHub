using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.Category
{
    // Include properties to be used as input for the query
    public record GetAllFlatCategoryQuery() : IRequest<Response<List<CategoryItemDTO>>>;
    public sealed class GetAllFlatCategoryQueryHandler(ICategoryRepository categoryRepository) :BaseResponseHandler, IRequestHandler<GetAllFlatCategoryQuery, Response<List<CategoryItemDTO>>>
    {
        public async Task<Response<List<CategoryItemDTO>>> Handle(GetAllFlatCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await categoryRepository.GetAllAsync(selector: (x,i) => new CategoryItemDTO
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            });
        return Success<List<CategoryItemDTO>>(result.ToList());
        }
    }

}
