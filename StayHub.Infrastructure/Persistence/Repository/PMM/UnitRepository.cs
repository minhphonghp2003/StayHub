using StayHub.Application.Interfaces.Repository.PMM;
using Unit = StayHub.Domain.Entity.PMM.Unit;
namespace StayHub.Infrastructure.Persistence.Repository.PMM;
public class UnitRepository(AppDbContext context) 
    : PagingAndSortingRepository<Unit>(context), IUnitRepository { }