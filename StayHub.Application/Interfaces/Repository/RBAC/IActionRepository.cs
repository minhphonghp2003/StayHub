using StayHub.Domain.Entity.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = StayHub.Domain.Entity.RBAC.Action;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IActionRepository :IRepository<Action>  
    {
    }
}
