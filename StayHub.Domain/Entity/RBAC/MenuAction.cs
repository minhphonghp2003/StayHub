using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Domain.Entity.RBAC
{
    public class MenuAction :BaseEntity
    {
        public int ActionId { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual Action Action { get; set; }
    }
}
