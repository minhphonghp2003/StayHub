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

        public async Task<(List<UserDTO>,int)> GetUserOfRole(int roleId, int pageNumber, int pageSize)
        {
            return (await GetManyPagedAsync(pageNumber: pageNumber,
                              pageSize: pageSize, filter: e => e.UserRoles.Any(j => j.RoleId == roleId), selector: (e, i) => new UserDTO
            {
                Id = e.Id,
                Username = e.Username,
                Fullname = e.Profile.Fullname,
                Phone = e.Profile.Phone,
                Image= e.Profile.Image,
            },include:e=>e.Include(j=>j.Profile)));
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

        public async Task<ProfileDTO> GetProfile(int userId)
        {
            var profile = await FindOneAsync(filter: e => e.Id == userId, selector: e => new ProfileDTO
            {
                Id = e.Id,
                Username = e.Username,
                Fullname = e.Profile.Fullname,
                Email = e.Profile.Email,
                Phone = e.Profile.Phone,
                Image = e.Profile.Image,
                IsActive = e.IsActive,
                Address = e.Profile.Address,
                Roles = e.UserRoles.Select(ur => new RoleDTO
                {
                    Id = ur.RoleId,
                    Name = ur.Role.Name,
                    Code = ur.Role.Code,
                    Description = ur.Role.Description,
                    
                }).ToList()
                
            },include: e => e.Include(j => j.Profile));
            return profile;
            


        }
    }

}
