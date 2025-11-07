using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Domain.Entity.Catalog;
using System.Net;

namespace StayHub.Application.CQRS.Catalog.Command.Category
{
    // Command
    public record AddCategoryCommand(string Name, string Code, string? Description)
        : IRequest<BaseResponse<CategoryDTO>>;

    // Handler
    public class AddCategoryCommandHandler(ICategoryRepository categoryRepository)
        : BaseResponseHandler, IRequestHandler<AddCategoryCommand, BaseResponse<CategoryDTO>>
    {
        public async Task<BaseResponse<CategoryDTO>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await categoryRepository.ExistsAsync(e => e.Code == request.Code))
            {

                return Failure<CategoryDTO>(message: "Duplicate CODE", code: HttpStatusCode.BadRequest);
            }
            var category = new StayHub.Domain.Entity.Catalog.Category
            {
                Name = request.Name,
                Code = request.Code,
                Description = request.Description
            };

            await categoryRepository.AddAsync(category);

            return Success(new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name ?? string.Empty,
                Code = category.Code,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            }, "Category created successfully", HttpStatusCode.Created);
        }
    }
}

