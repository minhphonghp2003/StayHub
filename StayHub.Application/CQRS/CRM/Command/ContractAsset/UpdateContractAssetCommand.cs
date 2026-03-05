using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.ContractAsset;
public class UpdateContractAssetCommand : IRequest<BaseResponse<ContractAssetDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public int ContractId { get; set; }
    public int AssetId { get; set; }
    public int Quantity { get; set; }
}
public sealed class UpdateContractAssetCommandHandler(IContractAssetRepository repository) : BaseResponseHandler, IRequestHandler<UpdateContractAssetCommand, BaseResponse<ContractAssetDTO>> 
{
    public async Task<BaseResponse<ContractAssetDTO>> Handle(UpdateContractAssetCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<ContractAssetDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.ContractId = request.ContractId;
        entity.AssetId = request.AssetId;
        entity.Quantity = request.Quantity;

        repository.Update(entity);
        return Success(new ContractAssetDTO 
        { 
            Id = entity.Id, 
            ContractId = entity.ContractId,
            AssetId = entity.AssetId,
            Quantity = entity.Quantity
        });
    }
}