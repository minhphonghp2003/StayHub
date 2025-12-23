using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Persistence.Repository.Catalog
{
    public class CategoryRepository : PagingAndSortingRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
