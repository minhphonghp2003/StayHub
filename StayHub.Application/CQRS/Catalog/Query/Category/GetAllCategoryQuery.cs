using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.Category
{
    public record GetAllCategoryQuery(int? pageNumber = null, int? pageSize = null, string? search = null) : IRequest<Response<CategoryDTO>>;

    public class GetAllCategoryQueryHandler(ICategoryRepository categoryRepository, IConfiguration _configuration)
        : BaseResponseHandler, IRequestHandler<GetAllCategoryQuery, Response<CategoryDTO>>
    {
        public async Task<Response<CategoryDTO>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.pageSize ?? _configuration.GetValue<int>("PageSize");
            var (categories, count) = await categoryRepository.GetManyPagedAsync(
                pageNumber: request.pageNumber ?? 1,
                pageSize: pageSize,
                filter: e => string.IsNullOrEmpty(request.search) || e.Name.Contains(request.search ?? string.Empty) || e.Code.Contains(request.search ?? ""),
                selector:(entity, index) => new CategoryDTO
                {
                    Id = entity.Id,
                    Name = entity.Name ?? string.Empty,
                    Code = entity.Code,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                });

            return SuccessPaginated(categories, count, pageSize, request.pageNumber ?? 1);
        }
    }
}

