using Shared.Common;
using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.TMS;

namespace StayHub.Domain.Entity.RBAC
{
    public class Action : BaseEntity
    {
        public string Path { get; set; }
        public bool AllowAnonymous { get; set; }
        public string Method { get; set; }
        public virtual List<MenuAction>? MenuActions { get; set; }
        public virtual List<RoleAction>? RoleActions { get; set; }
        public virtual List<Tier>? Tiers { get; set; }
    }
}
