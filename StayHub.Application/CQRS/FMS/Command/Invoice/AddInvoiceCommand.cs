using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;
using StayHub.Application.DTO.FMS;
using StayHub.Application.Interfaces.Repository.FMS;
using StayHub.Application.Interfaces.Repository.PMM;
using StayHub.Domain.Entity.FMS;
namespace StayHub.Application.CQRS.FMS.Command.Invoice;

public record AddInvoiceCommand(int UnitId, List<InvoiceServiceDTO> services, int ReasonId, DateTime Month, DateTime IssueDate, DateTime DueDate, DateTime FromDate, DateTime ToDate, string? Note, long? Discount) : IRequest<BaseResponse<bool>>;
public sealed class AddInvoiceCommandHandler(IInvoiceRepository repository, IServiceRepository serviceRepository, IUnitRepository unitRepository) : BaseResponseHandler, IRequestHandler<AddInvoiceCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(AddInvoiceCommand request, CancellationToken ct)
    {
        var contract = await unitRepository.FindOneAsync(e => e.Id == request.UnitId, e => e.Contract, include: e => e.Include(j => j.Contract));
        if (contract == null)
        {
            return Failure<bool>("No contract created", System.Net.HttpStatusCode.BadRequest);
        }
        
        double remainAmount = contract.Price;
        var serviceIds = request.services.Select(e => e.ServiceId).ToList();
        var servicePrices = await serviceRepository.GetManyAsync(e => serviceIds.Contains(e.Id), (e, i) => e.Price * request.services[i].Quantity);
        remainAmount += servicePrices.Sum();

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
            Discount = request.Discount,
            RemainAmount = remainAmount,
            Services =request.services.Select(e=>new InvoiceService
            {
                ServiceId = e.ServiceId,
                Quantity = e.Quantity,
            }).ToList(),
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}