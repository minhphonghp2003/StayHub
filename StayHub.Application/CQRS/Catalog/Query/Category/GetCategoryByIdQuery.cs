using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.Category
{
    public record GetCategoryByIdQuery(int Id) : IRequest<BaseResponse<CategoryDTO>>;

    internal sealed class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        : BaseResponseHandler, IRequestHandler<GetCategoryByIdQuery, BaseResponse<CategoryDTO>>
    {
        public async Task<BaseResponse<CategoryDTO>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await categoryRepository.FindOneAsync(
                filter: e => e.Id == request.Id,
                selector: e => new CategoryDTO
                {
                    Id = e.Id,
                    Name = e.Name ?? string.Empty,
                    Code = e.Code,
                    Description = e.Description,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                });

            if (result == null)
                return Failure<CategoryDTO>("Category not found", System.Net.HttpStatusCode.BadRequest);

            return Success(result);
        }
    }
}

