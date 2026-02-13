using StayHub.Domain.Entity.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Infrastructure.Persistence.Repository.TMS;

public class TierRepository(AppDbContext context) : Repository<Tier>(context), ITierRepository
{
}
