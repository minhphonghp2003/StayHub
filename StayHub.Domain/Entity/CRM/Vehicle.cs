using Shared.Common;
namespace StayHub.Domain.Entity.CRM;
public class Vehicle : BaseEntity 
{ 
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string LicensePlate { get; set; }
    public string? Image { get; set; }
}