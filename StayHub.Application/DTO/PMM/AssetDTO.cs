using Shared.Common;
namespace StayHub.Application.DTO.PMM;
public class AssetDTO : BaseDTO 
{ 
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int? Price { get; set; }
    public int TypeId { get; set; }
    public int PropertyId { get; set; }
    public int? UnitId { get; set; }
    public string? Note { get; set; }
    public string Image { get; set; }
}