using Shared.Common;
using StayHub.Domain.Entity.Catalog;
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
    public string Image { get; set; }
    //Nav
    public virtual CategoryItem Type { get; set; }
    public virtual Property Property { get; set; }
    public virtual Unit Unit { get; set; }
}