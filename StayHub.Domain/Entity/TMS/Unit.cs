using StayHub.Domain.Entity.Catalog;

namespace StayHub.Domain.Entity.TMS;

public class Unit : BaseEntity
{
    public int UnitGroupId { get; set; }
    public int StatusId { get; set; }
    public decimal BasePrice { get; set; }
    public bool IsDeleted { get; set; }
    public virtual CategoryItem Status { get; set; } 
    
    public virtual UnitGroup UnitGroup { get; set; } // Navigation property
}
