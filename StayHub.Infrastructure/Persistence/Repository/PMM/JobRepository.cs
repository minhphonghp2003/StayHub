using StayHub.Application.Interfaces.Repository.PMM;
using Job = StayHub.Domain.Entity.PMM.Job;
namespace StayHub.Infrastructure.Persistence.Repository.PMM;
public class JobRepository(AppDbContext context) 
    : PagingAndSortingRepository<Job>(context), IJobRepository { }