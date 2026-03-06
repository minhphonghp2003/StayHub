using Shared.Common;
using StayHub.Application.DTO.Catalog;
namespace StayHub.Application.DTO.PMM;
public class ServiceDTO : BaseDTO 
{ 
    public string Name { get; set; }
    public int PropertyId { get; set; }
    public CategoryItemDTO UnitType { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public long Price { get; set; }
}