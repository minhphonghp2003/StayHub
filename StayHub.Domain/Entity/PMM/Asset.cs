using Shared.Common;
using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.CRM;
namespace StayHub.Domain.Entity.PMM;
public class Asset : BaseEntity 
{ 
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int? Price { get; set; }
    public int TypeId { get; set; }
    public int PropertyId { get; set; }
    public int? UnitId { get; set; }
    public string? Note { get; set; }
    public string? Image { get; set; }
    //Nav
    public virtual CategoryItem Type { get; set; }
    public virtual List<ContractAsset> ContractAssets { get; set; }
}