using Job = StayHub.Domain.Entity.PMM.Job;
namespace StayHub.Application.Interfaces.Repository.PMM;
public interface IJobRepository : IPagingAndSortingRepository<Job> { }