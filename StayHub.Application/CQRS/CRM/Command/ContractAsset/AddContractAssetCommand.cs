using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.ContractAsset;
public record AddContractAssetCommand(int ContractId, int AssetId, int Quantity) : IRequest<BaseResponse<bool>>;
public sealed class AddContractAssetCommandHandler(IContractAssetRepository repository) : BaseResponseHandler, IRequestHandler<AddContractAssetCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddContractAssetCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.CRM.ContractAsset 
        { 
            ContractId = request.ContractId,
            AssetId = request.AssetId,
            Quantity = request.Quantity
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}