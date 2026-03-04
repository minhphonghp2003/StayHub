using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Asset;
public class UpdateAssetCommand : IRequest<BaseResponse<AssetDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public string? Name { get; set; }
}
public sealed class UpdateAssetCommandHandler(IAssetRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateAssetCommand, BaseResponse<AssetDTO>> 
{
    public async Task<BaseResponse<AssetDTO>> Handle(UpdateAssetCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<AssetDTO>("Not found", HttpStatusCode.BadRequest);
        entity.Name = request.Name ?? entity.Name;
        repository.Update(entity);
        return Success(new AssetDTO { Id = entity.Id, Name = entity.Name });
    }
}