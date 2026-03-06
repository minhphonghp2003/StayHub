using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.ContractService;
public class UpdateContractServiceCommand : IRequest<BaseResponse<ContractServiceDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public int ContractId { get; set; }
    public int ServiceId { get; set; }
    public int Quantity { get; set; }
}
public sealed class UpdateContractServiceCommandHandler(IContractServiceRepository repository) : BaseResponseHandler, IRequestHandler<UpdateContractServiceCommand, BaseResponse<ContractServiceDTO>> 
{
    public async Task<BaseResponse<ContractServiceDTO>> Handle(UpdateContractServiceCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<ContractServiceDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.ContractId = request.ContractId;
        entity.ServiceId = request.ServiceId;
        entity.Quantity = request.Quantity;

        repository.Update(entity);
        return Success(new ContractServiceDTO 
        { 
            Id = entity.Id, 
            ContractId = entity.ContractId,
            ServiceId = entity.ServiceId,
            Quantity = request.Quantity,
            
        });
    }
}