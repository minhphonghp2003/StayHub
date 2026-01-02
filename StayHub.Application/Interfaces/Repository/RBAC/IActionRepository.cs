using Action = StayHub.Domain.Entity.RBAC.Action;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IActionRepository : IPagingAndSortingRepository <Action>
    {
        Task<bool> AllowAnonymous(int id, bool allowAnonymous);
    }
}
