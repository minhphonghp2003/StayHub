using StayHub.Application.Interfaces.Repository.PMM;
using Service = StayHub.Domain.Entity.PMM.Service;
namespace StayHub.Infrastructure.Persistence.Repository.PMM;
public class ServiceRepository(AppDbContext context) : PagingAndSortingRepository<Service>(context), IServiceRepository { }