using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.PMM;
namespace StayHub.Domain.Entity.PMM;

public class Service : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    //Loai dich vu
    public int FeeCategoryId { get; set; }
    //Loai don gia
    public int TypeId { get; set; }
    //Loai thue
    public int VatTypeId { get; set; }
    public int PropertyId { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    //Nav properties
    public virtual CategoryItem FeeCategory { get; set; }
    public virtual CategoryItem Type { get; set; }
    public virtual CategoryItem VatType { get; set; }
    public virtual Property Property { get; set; }
}