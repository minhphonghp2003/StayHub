using Shared.Common;

namespace StayHub.Domain.Entity.RBAC
{
    public class Action : BaseEntity
    {
        public string Path { get; set; }
        public bool AllowAnonymous { get; set; }
        public HttpVerb Method { get; set; }
        public virtual List<Menu>? Menus { get; set; }
        public virtual List<Role>? Roles { get; set; }
    }
}
