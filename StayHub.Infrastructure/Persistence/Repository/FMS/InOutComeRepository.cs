using StayHub.Application.Interfaces.Repository.FMS;
using InOutCome = StayHub.Domain.Entity.FMS.InOutCome;
namespace StayHub.Infrastructure.Persistence.Repository.FMS;
public class InOutComeRepository(AppDbContext context) : PagingAndSortingRepository<InOutCome>(context), IInOutComeRepository { }