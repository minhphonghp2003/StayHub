using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Common;

namespace StayHub.Infrastructure.Persistence.Repository.Catalog
{
    public class CategoryRepository : PagingAndSortingRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public bool ExistItemAsync(int itemId, CategoryCode code)
        {
           return  _dbSet.Any(x => x.Code== code.ToString() && x.CategoryItems !=null && x.CategoryItems.Any(item=>item.Id==itemId) );
        }
    }
}
