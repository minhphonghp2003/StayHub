using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Query.ContractService;
public record GetAllContractServiceQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<ContractServiceDTO>>;
public sealed class GetAllContractServiceQueryHandler(IContractServiceRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllContractServiceQuery, Response<ContractServiceDTO>> 
{
    public async Task<Response<ContractServiceDTO>> Handle(GetAllContractServiceQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => request.searchKey == null,
            selector: (x, i) => new ContractServiceDTO 
            { 
                Id = x.Id, 
                ContractId = x.ContractId,
                ServiceId = x.ServiceId,
                Quantity = x.Quantity,
            }
        );
        return SuccessPaginated(result.ToList(), count, request.pageNumber ?? 1, size);
    }
}