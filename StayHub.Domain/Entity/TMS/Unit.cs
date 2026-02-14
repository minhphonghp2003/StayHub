using StayHub.Domain.Entity.Catalog;

namespace StayHub.Domain.Entity.TMS;

public class Unit : BaseEntity
{
    public int PropertyId { get; set; }
    public decimal BasePrice { get; set; }
    public CategoryItem Status { get; set; } 
    public bool IsDeleted { get; set; }
    
    public Property Property { get; set; } // Navigation property
}
