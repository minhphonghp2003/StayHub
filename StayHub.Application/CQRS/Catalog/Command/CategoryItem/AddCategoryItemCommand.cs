using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Domain.Entity.Catalog;
using System.Net;

namespace StayHub.Application.CQRS.Catalog.Command.CategoryItem
{
    // Command
    public record AddCategoryItemCommand(string Name, string Code, string? Value, string? Icon, int? CategoryId)
        : IRequest<BaseResponse<CategoryItemDTO>>;

    // Handler
    public class AddCategoryItemCommandHandler(ICategoryItemRepository categoryItemRepository)
        : BaseResponseHandler, IRequestHandler<AddCategoryItemCommand, BaseResponse<CategoryItemDTO>>
    {
        public async Task<BaseResponse<CategoryItemDTO>> Handle(AddCategoryItemCommand request, CancellationToken cancellationToken)
        {
            if (await categoryItemRepository.ExistsAsync(e => e.Code == request.Code))
            {

                return Failure<CategoryItemDTO>(message: "Duplicate CODE", code: HttpStatusCode.BadRequest);
            }
            var item = new Domain.Entity.Catalog.CategoryItem
            {
                Name = request.Name,
                Code = request.Code,
                Value = request.Value,
                Icon = request.Icon,
                CategoryId = request.CategoryId
            };

            await categoryItemRepository.AddAsync(item);

            return Success(new CategoryItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code,
                Value = item.Value,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            }, "CategoryItem created successfully", HttpStatusCode.Created);
        }
    }
}

