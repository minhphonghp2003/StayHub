using Shared.Common;
namespace StayHub.Application.DTO.CRM;
public class CustomerDTO : BaseDTO 
{ 
    public string Name { get; set; }
    public string Phone { get; set; }
    public string? Email { get; set; }
    public string? CCCD { get; set; }
    public int? GenderId { get; set; }
    public int? ProvinceId { get; set; }
    public int? WardId { get; set; }
    public int? UnitId { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? Image { get; set; }
    public string? Job { get; set; }
}