using Shared.Common;

namespace StayHub.Domain.Entity.RBAC
{
    public class Action : BaseEntity
    {
        public string Path { get; set; }
        public bool AllowAnonymous { get; set; }
        public string Method { get; set; }
        public virtual List<MenuAction>? MenuActions { get; set; }
        public virtual List<RoleAction>? RoleActions { get; set; }
    }
}
