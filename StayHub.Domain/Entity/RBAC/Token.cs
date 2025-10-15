using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Domain.Entity.RBAC
{
    public class Token:BaseEntity
    {
        public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime? RevokedAt { get; set; }
        public int UserId { get; set; }
    }
}
