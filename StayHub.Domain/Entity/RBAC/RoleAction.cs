using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Domain.Entity.RBAC
{
    public class RoleAction :BaseEntity
    {
        public int ActionId { get; set; }
        public int RoleId { get; set; }
        public virtual Action Action { get; set; }
        public virtual Role Role { get; set; }
    }
}
