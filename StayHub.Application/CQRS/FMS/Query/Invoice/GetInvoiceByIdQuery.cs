using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.FMS;
using StayHub.Application.Interfaces.Repository.FMS;
namespace StayHub.Application.CQRS.FMS.Query.Invoice;
public record GetInvoiceByIdQuery(int Id) : IRequest<BaseResponse<InvoiceDTO>>;
public sealed class GetInvoiceByIdQueryHandler(IInvoiceRepository repository) : BaseResponseHandler, IRequestHandler<GetInvoiceByIdQuery, BaseResponse<InvoiceDTO>> 
{
    public async Task<BaseResponse<InvoiceDTO>> Handle(GetInvoiceByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new InvoiceDTO 
        { 
            Id = x.Id, 
            UnitId = x.UnitId,
            ReasonId = x.ReasonId,
            Month = x.Month,
            IssueDate = x.IssueDate,
            DueDate = x.DueDate,
            FromDate = x.FromDate,
            ToDate = x.ToDate,
            Note = x.Note,
            Discount = x.Discount
        });
        return result == null ? Failure<InvoiceDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}