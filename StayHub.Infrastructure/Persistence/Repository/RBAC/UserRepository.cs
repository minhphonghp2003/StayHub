using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using System;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class UserRepository : PagingAndSortingRepository<User>, IUserRepository
    {


        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public Task<bool> ExistsByUsernameAsync(string Username)
        {
            return _dbSet.AnyAsync(u => u.Username == Username);
        }

        public async Task<(List<UserDTO>, int)> GetAllUser(int pageNumber, int pageSize, string? searchKey)
        {
            return await GetManyPagedAsync(
                              pageNumber: pageNumber,
                              pageSize: pageSize,
                              filter: x => searchKey == null || x.Profile.Fullname.Contains(searchKey) || x.Username.Contains(searchKey) || x.Profile.Phone.Contains(searchKey) || x.Profile.Email.Contains(searchKey),
                              include: x => x.Include(u => u.Profile),
                              selector: (x, i) => new UserDTO
                              {
                                  Id = x.Id,
                                  Username = x.Username,
                                  IsActive = x.IsActive,
                                  Fullname = x.Profile.Fullname,
                                  Email = x.Profile.Email,
                                  Phone = x.Profile.Phone,
                                  Image = x.Profile.Image,
                              }
                              );
        }


        public async Task<bool> SetActivated(int id, bool activated)
        {
            var user = new User
            {
                Id = id,
                IsActive = activated
            };
            _appDbContext.Attach(user);
            _appDbContext.Entry(user).Property(x => x.IsActive).IsModified = true;
            await SaveAsync();
            return activated;
        }
    }

}
