using Customer = StayHub.Domain.Entity.CRM.Customer;
namespace StayHub.Application.Interfaces.Repository.CRM;
public interface ICustomerRepository : IPagingAndSortingRepository<Customer> { }