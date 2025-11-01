using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class UserRoleRepository : Repository<UserRole>,IUserRoleRepository 
    {

        public UserRoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRolesOfUser(int userId)
        {
            var result = await GetManyAsync(filter: e => e.UserId == userId, selector: (e, i) => new RoleDTO
            {
                Id = e.Role.Id,
                Code = e.Role.Code,
                Name = e.Role.Name,
                Description = e.Role.Description,
                CreatedAt = e.Role.CreatedAt,
                UpdatedAt = e.Role.UpdatedAt

            }, include: e=>e.Include(j=>j.Role), tracking: false) ;
            return result;
        }
    }
}
