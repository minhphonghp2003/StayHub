using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Query.ContractService;
public record GetContractServiceByIdQuery(int Id) : IRequest<BaseResponse<ContractServiceDTO>>;
public sealed class GetContractServiceByIdQueryHandler(IContractServiceRepository repository) : BaseResponseHandler, IRequestHandler<GetContractServiceByIdQuery, BaseResponse<ContractServiceDTO>> 
{
    public async Task<BaseResponse<ContractServiceDTO>> Handle(GetContractServiceByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new ContractServiceDTO 
        { 
            Id = x.Id, 
            ContractId = x.ContractId,
            ServiceId = x.ServiceId,
            Quantity = x.Quantity,
        });
        return result == null ? Failure<ContractServiceDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}