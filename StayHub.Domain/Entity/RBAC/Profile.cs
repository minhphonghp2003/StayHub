using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Domain.Entity.RBAC
{
    public class Profile : BaseEntity
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public virtual User? User { get; set; }
    }
}
