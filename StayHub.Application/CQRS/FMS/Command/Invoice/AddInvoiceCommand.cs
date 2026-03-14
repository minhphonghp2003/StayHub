using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.FMS;
namespace StayHub.Application.CQRS.FMS.Command.Invoice;
public record AddInvoiceCommand(int UnitId, int ReasonId, DateTime Month, DateTime IssueDate, DateTime DueDate, DateTime FromDate, DateTime ToDate, string? Note, long? Discount) : IRequest<BaseResponse<bool>>;
public sealed class AddInvoiceCommandHandler(IInvoiceRepository repository) : BaseResponseHandler, IRequestHandler<AddInvoiceCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddInvoiceCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.FMS.Invoice 
        { 
            UnitId = request.UnitId,
            ReasonId = request.ReasonId,
            Month = request.Month,
            IssueDate = request.IssueDate,
            DueDate = request.DueDate,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            Note = request.Note,
            Discount = request.Discount
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}