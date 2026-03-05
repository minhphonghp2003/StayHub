using StayHub.Application.Interfaces.Repository.CRM;
using Customer = StayHub.Domain.Entity.CRM.Customer;
namespace StayHub.Infrastructure.Persistence.Repository.CRM;
public class CustomerRepository(AppDbContext context) : PagingAndSortingRepository<Customer>(context), ICustomerRepository { }