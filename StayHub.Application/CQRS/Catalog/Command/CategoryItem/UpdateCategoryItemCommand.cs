using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Domain.Entity.Catalog;
using System.Net;
using System.Text.Json.Serialization;

namespace StayHub.Application.CQRS.Catalog.Command.CategoryItem
{
    public class UpdateCategoryItemCommand : IRequest<BaseResponse<CategoryItemDTO>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Value { get; set; }
        public string? Icon { get; set; }
        public int? CategoryId { get; set; }

        public UpdateCategoryItemCommand(int id, string name, string code, string? value, string? icon, int? categoryId)
        {
            Id = id;
            Name = name;
            Code = code;
            Value = value;
            Icon = icon;
            CategoryId = categoryId;
        }

        public UpdateCategoryItemCommand() { }
    }

    public sealed class UpdateCategoryItemCommandHandler(ICategoryItemRepository categoryItemRepository)
        : BaseResponseHandler, IRequestHandler<UpdateCategoryItemCommand, BaseResponse<CategoryItemDTO>>
    {
        public async Task<BaseResponse<CategoryItemDTO>> Handle(UpdateCategoryItemCommand request, CancellationToken cancellationToken)
        {
            if (await categoryItemRepository.ExistsAsync(e => e.Code == request.Code))
            {
                return Failure<CategoryItemDTO>(message: "Duplicate CODE", code: HttpStatusCode.BadRequest);
            }
            var item = await categoryItemRepository.FindOneEntityAsync(e => e.Id == request.Id);
            if (item == null)
                return Failure<CategoryItemDTO>(message: "No category item found", code: HttpStatusCode.BadRequest);

            item.Name = request.Name;
            item.Code = request.Code;
            item.Value = request.Value;
            item.Icon = request.Icon;
            item.CategoryId = request.CategoryId;


            return Success(new CategoryItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code,
                Value = item.Value,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            }, "CategoryItem updated successfully");
        }
    }
}

