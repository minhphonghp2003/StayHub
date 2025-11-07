using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Domain.Entity.Catalog;
using System.Net;
using System.Text.Json.Serialization;

namespace StayHub.Application.CQRS.Catalog.Command.Category
{
    public class UpdateCategoryCommand : IRequest<BaseResponse<CategoryDTO>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }

        public UpdateCategoryCommand(int id, string name, string code, string? description)
        {
            Id = id;
            Name = name;
            Code = code;
            Description = description;
        }

        public UpdateCategoryCommand() { }
    }

    public sealed class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        : BaseResponseHandler, IRequestHandler<UpdateCategoryCommand, BaseResponse<CategoryDTO>>
    {
        public async Task<BaseResponse<CategoryDTO>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await categoryRepository.ExistsAsync(e => e.Code == request.Code))
            {

                return Failure<CategoryDTO>(message: "Duplicate CODE", code: HttpStatusCode.BadRequest);
            }
            var category = await categoryRepository.FindOneEntityAsync(e=>e.Id==request.Id);
            if (category == null)
            {
                return Failure<CategoryDTO>(message: "No category found", code: HttpStatusCode.BadRequest);
            }

            category.Name = request.Name;
            category.Code = request.Code;
            category.Description = request.Description;

            categoryRepository.Update(category);
            return Success(new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name ?? string.Empty,
                Code = category.Code,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            }, "Category updated successfully");
        }
    }
}

