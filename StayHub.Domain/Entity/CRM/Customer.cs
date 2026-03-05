using Shared.Common;
using StayHub.Domain.Entity.PMM;
using StayHub.Domain.Entity.RBAC;
namespace StayHub.Domain.Entity.CRM;
public class Customer : BaseEntity 
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
    public bool IsRepresentative { get; set; }
    public int? UserId { get; set; }

    //Nav
    public virtual User User { get; set; }
    public virtual Unit? Unit { get; set; }
    
}