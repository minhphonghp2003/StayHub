namespace StayHub.Domain.Entity.RBAC
{
    public class Role : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual List<Action>? Actions { get; set; }
        public virtual List<UserRole>? UserRoles { get; set; }
    }
}
