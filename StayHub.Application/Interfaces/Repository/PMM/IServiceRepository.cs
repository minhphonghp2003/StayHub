using Service = StayHub.Domain.Entity.PMM.Service;
namespace StayHub.Application.Interfaces.Repository.PMM;
public interface IServiceRepository : IPagingAndSortingRepository<Service> { }