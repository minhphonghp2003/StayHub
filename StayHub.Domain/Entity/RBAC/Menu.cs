namespace StayHub.Domain.Entity.RBAC
{
    public class Menu : BaseEntity
    {
        public string Path { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public int? ParentId { get; set; }
        public bool? IsActive { get; set; }

        public List<Menu>? SubMenus { get; set; }
        public List<Action>? Actions { get; set; }
    }
}
