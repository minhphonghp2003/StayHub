using StayHub.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.Interfaces.Repository
{
    public interface IPagingAndSortingRepository<T> : IRepository<T> where T : BaseEntity
    {
    }
}
