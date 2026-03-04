using StayHub.Application.Interfaces.Repository.PMM;
using UnitGroup = StayHub.Domain.Entity.PMM.UnitGroup;
namespace StayHub.Infrastructure.Persistence.Repository.PMM;
public class UnitGroupRepository(AppDbContext context) 
    : PagingAndSortingRepository<UnitGroup>(context), IUnitGroupRepository { }