using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.ContractService;
public record DeleteContractServiceCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteContractServiceCommandHandler(IContractServiceRepository repository) : BaseResponseHandler, IRequestHandler<DeleteContractServiceCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteContractServiceCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.CRM.ContractService { Id = request.Id });
        return Success(true);
    }
}