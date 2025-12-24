using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.CategoryItem
{
    public record GetCategoryItemByIdQuery(int Id) : IRequest<BaseResponse<CategoryItemDTO>>;

    internal sealed class GetCategoryItemByIdQueryHandler(ICategoryItemRepository categoryItemRepository)
        : BaseResponseHandler, IRequestHandler<GetCategoryItemByIdQuery, BaseResponse<CategoryItemDTO>>
    {
        public async Task<BaseResponse<CategoryItemDTO>> Handle(GetCategoryItemByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await categoryItemRepository.FindOneAsync(
                filter: e => e.Id == request.Id,
                selector: e => new CategoryItemDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Code = e.Code,
                    Value = e.Value,
                    Icon = e.Icon,
                    CategoryId = e.CategoryId,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                });

            if (result == null)
                return Failure<CategoryItemDTO>("CategoryItem not found", System.Net.HttpStatusCode.BadRequest);

            return Success(result);
        }
    }
}

