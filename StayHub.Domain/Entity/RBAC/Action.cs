using Shared.Common;

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
