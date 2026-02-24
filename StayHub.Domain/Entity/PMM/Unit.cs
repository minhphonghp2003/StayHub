using Shared.Common;
using StayHub.Domain.Entity.Catalog;

namespace StayHub.Domain.Entity.TMS;

public class Unit : BaseEntity
{
    public string Name { get; set; }
    public int UnitGroupId { get; set; }
    public UnitStatus Status{ get; set; }
    public decimal BasePrice { get; set; }
    public bool IsDeleted { get; set; }
    
    public virtual UnitGroup UnitGroup { get; set; } // Navigation property
}
