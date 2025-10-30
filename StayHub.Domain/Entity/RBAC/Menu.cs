namespace StayHub.Domain.Entity.RBAC
{
    public class Menu : BaseEntity
    {
        public  string Name { get; set; }
        public string Path { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public int? ParentId { get; set; }
        public bool? IsActive { get; set; }

        public virtual List<Menu>? SubMenus { get; set; }
        public virtual List<Action>? Actions { get; set; }
    }
}
