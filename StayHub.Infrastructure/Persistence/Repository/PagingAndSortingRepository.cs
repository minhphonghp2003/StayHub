using StayHub.Application.Interfaces.Repository;
using StayHub.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Persistence.Repository
{
    public class PagingAndSortingRepository<T> : Repository<T>, IPagingAndSortingRepository<T> where T : BaseEntity
    {
        public PagingAndSortingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
