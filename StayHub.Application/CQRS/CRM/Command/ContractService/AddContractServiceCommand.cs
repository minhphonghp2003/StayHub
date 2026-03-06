using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.ContractService;
public record AddContractServiceCommand(int ContractId, int ServiceId, int Quantity) : IRequest<BaseResponse<bool>>;
public sealed class AddContractServiceCommandHandler(IContractServiceRepository repository) : BaseResponseHandler, IRequestHandler<AddContractServiceCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddContractServiceCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.CRM.ContractService 
        { 
            ContractId = request.ContractId,
            ServiceId = request.ServiceId,
            Quantity = request.Quantity
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}