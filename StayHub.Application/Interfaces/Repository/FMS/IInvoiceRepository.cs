using Invoice = StayHub.Domain.Entity.FMS.Invoice;
namespace StayHub.Application.Interfaces.Repository.FMS;
public interface IInvoiceRepository : IPagingAndSortingRepository<Invoice> { }