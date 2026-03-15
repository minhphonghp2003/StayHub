using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.FMS;
using StayHub.Application.Interfaces.Repository.FMS;
using Microsoft.EntityFrameworkCore;
namespace StayHub.Application.CQRS.FMS.Query.Invoice;

public record GetInvoiceByIdQuery(int Id) : IRequest<BaseResponse<InvoiceDTO>>;
public sealed class GetInvoiceByIdQueryHandler(IInvoiceRepository repository) : BaseResponseHandler, IRequestHandler<GetInvoiceByIdQuery, BaseResponse<InvoiceDTO>>
{
    public async Task<BaseResponse<InvoiceDTO>> Handle(GetInvoiceByIdQuery request, CancellationToken ct)
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, include: x => x.Include(j => j.Unit).Include(j => j.Reason).Include(j => j.Services),
        selector: x => new InvoiceDTO
        {
            Id = x.Id,
            UnitId = x.UnitId,
            Unit = new DTO.PMM.UnitDTO
            {
                Name = x.Unit.Name,
            },
            ReasonId = x.ReasonId,
            Reason = new DTO.Catalog.CategoryItemDTO
            {
                Name = x.Reason.Name,
            },
            Services = x.Services == null ? null : x.Services.Select(e => new InvoiceServiceDTO
            {
                ServiceId = e.ServiceId,
                Quantity = e.Quantity,
            }).ToList(),
            Month = x.Month,
            IssueDate = x.IssueDate,
            DueDate = x.DueDate,
            FromDate = x.FromDate,
            ToDate = x.ToDate,
            Note = x.Note,
            Discount = x.Discount,
        });
        return result == null ? Failure<InvoiceDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}