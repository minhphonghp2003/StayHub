using StayHub.Application.DTO.Catalog;

namespace StayHub.Application.DTO.TMS;

public class UnitDTO : BaseDTO
{
    public int PropertyId { get; set; }
    public decimal BasePrice { get; set; }
    public CategoryItemDTO Status { get; set; } 
    public bool IsDeleted { get; set; }
    public PropertyDTO Property { get; set; } 
}
