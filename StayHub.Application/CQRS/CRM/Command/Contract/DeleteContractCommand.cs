using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Contract;
public record DeleteContractCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteContractCommandHandler(IContractRepository repository) : BaseResponseHandler, IRequestHandler<DeleteContractCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteContractCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.CRM.Contract { Id = request.Id });
        return Success(true);
    }
}