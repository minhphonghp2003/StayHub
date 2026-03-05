using Shared.Common;
using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.CRM;
namespace StayHub.Domain.Entity.PMM;
public class Service : BaseEntity 
{ 
    public string Name { get; set; }
    public int PropertyId { get; set; }
    public int UnitTypeId { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public string UnitName { get; set; }
    public long Price { get; set; }
    //Nav
    public virtual List<ContractService> ContractServices { get; set; }
    public virtual CategoryItem UnitType { get; set; }
}