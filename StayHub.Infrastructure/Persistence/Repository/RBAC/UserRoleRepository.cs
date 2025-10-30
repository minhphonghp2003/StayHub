using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class UserRoleRepository : Repository<UserRole>,IUserRoleRepository 
    {

        public UserRoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
