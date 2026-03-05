using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Customer;
public record DeleteCustomerCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteCustomerCommandHandler(ICustomerRepository repository) : BaseResponseHandler, IRequestHandler<DeleteCustomerCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteCustomerCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.CRM.Customer { Id = request.Id });
        return Success(true);
    }
}