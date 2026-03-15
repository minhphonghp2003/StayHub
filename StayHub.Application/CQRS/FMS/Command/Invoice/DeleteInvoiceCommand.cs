using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.FMS;
namespace StayHub.Application.CQRS.FMS.Command.Invoice;

public record DeleteInvoiceCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteInvoiceCommandHandler(IInvoiceRepository repository) : BaseResponseHandler, IRequestHandler<DeleteInvoiceCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteInvoiceCommand request, CancellationToken ct)
    {

        await repository.DeleteWhere(e => e.Id == request.Id && e.Status != Shared.Common.InvoiceStatus.Active);
        return Success(true);
    }
}