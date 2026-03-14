using StayHub.Application.Interfaces.Repository.FMS;
using Invoice = StayHub.Domain.Entity.FMS.Invoice;
namespace StayHub.Infrastructure.Persistence.Repository.FMS;
public class InvoiceRepository(AppDbContext context) : PagingAndSortingRepository<Invoice>(context), IInvoiceRepository { }