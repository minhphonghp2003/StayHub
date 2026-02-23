using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using Shared.Common;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Property;

public class UpdatePropertyCommand : IRequest<BaseResponse<PropertyDTO>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public int TypeId { get; set; }
    public string? Image { get; set; }
    public int? WardId { get; set; }
    public int? ProvinceId { get; set; }
    public UpdatePropertyCommand() { }

    public UpdatePropertyCommand(  string? name, string? address, int typeId, string? image, int? wardId, int? provinceId)
    {
        Name = name;
        Address = address;
        TypeId = typeId;
        Image = image;
        WardId = wardId;
        ProvinceId = provinceId;
    }
}

public sealed class UpdatePropertyCommandHandler(IPropertyRepository repository,ICategoryRepository categoryRepository) 
    : BaseResponseHandler, IRequestHandler<UpdatePropertyCommand, BaseResponse<PropertyDTO>>
{
    public async Task<BaseResponse<PropertyDTO>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        if (!categoryRepository.ExistItemAsync(request.TypeId, CategoryCode.PROPERTY_TYPE))
        {
            return Failure<PropertyDTO>(message: "Invalid Type", code: System.Net.HttpStatusCode.BadRequest);
        }
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<PropertyDTO>("Not found", HttpStatusCode.BadRequest);
        entity.Name = request.Name ?? entity.Name;
        entity.Address = request.Address ?? entity.Address;
        entity.TypeId = request.TypeId;
        entity.Image = request.Image ?? entity.Image;
        entity.WardId = request.WardId ?? entity.WardId;
        entity.ProvinceId = request.ProvinceId ?? entity.ProvinceId;
        repository.Update(entity);
        return Success(new PropertyDTO { Id = entity.Id , Name = entity.Name, Address = entity.Address,  Image = entity.Image, WardId = entity.WardId, ProvinceId = entity.ProvinceId});
        
    }
}
