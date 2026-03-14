using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.FMS;
using StayHub.Application.Interfaces.Repository.FMS;
namespace StayHub.Application.CQRS.FMS.Query.Invoice;
public record GetAllInvoiceQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<InvoiceDTO>>;
public sealed class GetAllInvoiceQueryHandler(IInvoiceRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllInvoiceQuery, Response<InvoiceDTO>> 
{
    public async Task<Response<InvoiceDTO>> Handle(GetAllInvoiceQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => (request.searchKey == null),
            selector: (x, i) => new InvoiceDTO 
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
            }
        );
        return SuccessPaginated(result.ToList(), count,size, request.pageNumber ?? 1);
    }
}