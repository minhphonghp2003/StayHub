using Shared.Common;
namespace StayHub.Application.DTO.CRM;
public class VehicleDTO : BaseDTO 
{ 
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string LicensePlate { get; set; }
    public string? Image { get; set; }
}