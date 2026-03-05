using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Query.ContractAsset;
public record GetContractAssetByIdQuery(int Id) : IRequest<BaseResponse<ContractAssetDTO>>;
public sealed class GetContractAssetByIdQueryHandler(IContractAssetRepository repository) : BaseResponseHandler, IRequestHandler<GetContractAssetByIdQuery, BaseResponse<ContractAssetDTO>> 
{
    public async Task<BaseResponse<ContractAssetDTO>> Handle(GetContractAssetByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new ContractAssetDTO 
        { 
            Id = x.Id, 
            ContractId = x.ContractId,
            AssetId = x.AssetId,
            Quantity = x.Quantity
        });
        return result == null ? Failure<ContractAssetDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}