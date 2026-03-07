using Shared.Common;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.DTO.PMM;
namespace StayHub.Application.DTO.CRM;
public class CustomerDTO : BaseDTO 
{ 
    public string Name { get; set; }
    public string Phone { get; set; }
    public string? Email { get; set; }
    public string? CCCD { get; set; }
    public int? GenderId { get; set; }
    public CategoryItemDTO? Gender { get; set; }
    public int? ProvinceId { get; set; }
    public ProvinceDTO? Province { get; set; }
    public int? WardId { get; set; }
    public WardDTO? Ward { get; set; }
    public int? UnitId { get; set; }
    public UnitDTO? Unit { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? Image { get; set; }
    public string? Job { get; set; }
}