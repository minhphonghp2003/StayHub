using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using Microsoft.EntityFrameworkCore;
namespace StayHub.Application.CQRS.PMM.Command.Asset;
public class UpdateAssetCommand : IRequest<BaseResponse<AssetDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int? Price { get; set; }
    public int TypeId { get; set; }
    public int PropertyId { get; set; }
    public int? UnitId { get; set; }
    public string? Note { get; set; }
    public string Image { get; set; }
}
public sealed class UpdateAssetCommandHandler(IAssetRepository repository) : BaseResponseHandler, IRequestHandler<UpdateAssetCommand, BaseResponse<AssetDTO>> 
{
    public async Task<BaseResponse<AssetDTO>> Handle(UpdateAssetCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id,include:e=>e.Include(j=>j.Type));
        if (entity == null) return Failure<AssetDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.Name = request.Name;
        entity.Quantity = request.Quantity;
        entity.Price = request.Price;
        entity.TypeId = request.TypeId;
        entity.PropertyId = request.PropertyId;
        entity.UnitId = request.UnitId;
        entity.Note = request.Note;
        entity.Image = request.Image;

        repository.Update(entity);
        return Success(new AssetDTO 
        { 
            Id = entity.Id, 
            Name = entity.Name,
            Quantity = entity.Quantity,
            Price = entity.Price,
            Type = new DTO.Catalog.CategoryItemDTO
            {
                Name = entity.Type.Name,
            },
            PropertyId = entity.PropertyId,
            UnitId = entity.UnitId,
            Note = entity.Note,
            Image = entity.Image
        });
    }
}