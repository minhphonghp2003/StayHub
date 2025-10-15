using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Domain.Entity.RBAC
{
    public class Action : BaseEntity
    {
        public string Path { get; set; }
        public bool AllowAnonymous { get; set; }
        public HttpVerb Method { get; set; }
        public List<Menu>? Menus { get; set; }
        public List<Role>? Roles { get; set; }
    }
}
