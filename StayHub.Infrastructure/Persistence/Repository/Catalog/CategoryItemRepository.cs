using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Domain.Entity.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Persistence.Repository.Catalog
{
    public class CategoryItemRepository : Repository<CategoryItem>, ICategoryItemRepository
    {
        public CategoryItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
