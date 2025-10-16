using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = StayHub.Domain.Entity.RBAC.Action;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class ActionRepository : Repository<Action>, IActionRepository
    {
        public ActionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
