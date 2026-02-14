using StayHub.Domain.Entity.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Common;

namespace StayHub.Application.Interfaces.Repository.Catalog
{
    public interface ICategoryRepository : IPagingAndSortingRepository<Category>
    {
        bool ExistItemAsync(int itemId,CategoryCode code);
    }
}
