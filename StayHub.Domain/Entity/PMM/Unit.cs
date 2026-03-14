using Shared.Common;
using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.CRM;

namespace StayHub.Domain.Entity.PMM;

public class Unit : BaseEntity
{
    public string Name { get; set; }
    public int UnitGroupId { get; set; }
    public UnitStatus Status{ get; set; }
    public decimal BasePrice { get; set; }
    public int MaximumCustomer { get; set; }
    public bool IsActive { get; set; }

    public virtual UnitGroup UnitGroup { get; set; } // Navigation property
    public virtual Contract? Contract { get; set; }

}
