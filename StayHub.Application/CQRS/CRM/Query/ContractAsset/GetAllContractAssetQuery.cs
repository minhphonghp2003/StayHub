using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Query.ContractAsset;
public record GetAllContractAssetQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<ContractAssetDTO>>;
public sealed class GetAllContractAssetQueryHandler(IContractAssetRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllContractAssetQuery, Response<ContractAssetDTO>> 
{
    public async Task<Response<ContractAssetDTO>> Handle(GetAllContractAssetQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => request.searchKey == null || x.Name.Contains(request.searchKey),
            selector: (x, i) => new ContractAssetDTO 
            { 
                Id = x.Id, 
                ContractId = x.ContractId,
                AssetId = x.AssetId,
                Quantity = x.Quantity
            }
        );
        return SuccessPaginated(result.ToList(), count, request.pageNumber ?? 1, size);
    }
}