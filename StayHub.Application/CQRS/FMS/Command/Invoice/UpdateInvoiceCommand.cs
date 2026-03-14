using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.FMS;
using StayHub.Application.Interfaces.Repository.FMS;
namespace StayHub.Application.CQRS.FMS.Command.Invoice;
public class UpdateInvoiceCommand : IRequest<BaseResponse<InvoiceDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public int UnitId { get; set; }
    public int ReasonId { get; set; }
    public DateTime Month { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? Note { get; set; }
    public long? Discount { get; set; }
}
public sealed class UpdateInvoiceCommandHandler(IInvoiceRepository repository) : BaseResponseHandler, IRequestHandler<UpdateInvoiceCommand, BaseResponse<InvoiceDTO>> 
{
    public async Task<BaseResponse<InvoiceDTO>> Handle(UpdateInvoiceCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<InvoiceDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.UnitId = request.UnitId;
        entity.ReasonId = request.ReasonId;
        entity.Month = request.Month;
        entity.IssueDate = request.IssueDate;
        entity.DueDate = request.DueDate;
        entity.FromDate = request.FromDate;
        entity.ToDate = request.ToDate;
        entity.Note = request.Note;
        entity.Discount = request.Discount;

        repository.Update(entity);
        return Success(new InvoiceDTO 
        { 
            Id = entity.Id, 
            UnitId = entity.UnitId,
            ReasonId = entity.ReasonId,
            Month = entity.Month,
            IssueDate = entity.IssueDate,
            DueDate = entity.DueDate,
            FromDate = entity.FromDate,
            ToDate = entity.ToDate,
            Note = entity.Note,
            Discount = entity.Discount
        });
    }
}