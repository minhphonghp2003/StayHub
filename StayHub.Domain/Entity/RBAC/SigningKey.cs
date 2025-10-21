using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Domain.Entity.RBAC
{
    public class SigningKey : BaseEntity
    {
        public bool IsActive { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
