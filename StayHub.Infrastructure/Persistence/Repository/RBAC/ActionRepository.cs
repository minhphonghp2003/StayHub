using StayHub.Application.Interfaces.Repository;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using Action = StayHub.Domain.Entity.RBAC.Action;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class ActionRepository : PagingAndSortingRepository<Action>, IActionRepository
    {
        public ActionRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<bool> AllowAnonymous(int id, bool allowAnonymous)
        {
            var action = new Action
            {
                Id = id,
                AllowAnonymous = allowAnonymous
            };
            _appDbContext.Attach(action);
            _appDbContext.Entry(action).Property(x => x.AllowAnonymous ).IsModified = true;
            await SaveAsync();
            return allowAnonymous;
        }
    }
}
