using Microsoft.EntityFrameworkCore;
using Shared.Common;
using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.PMM;
using StayHub.Domain.Entity.RBAC;
using StayHub.Domain.Entity.TMS;

namespace StayHub.Domain.Entity.PMM;

public class Property: BaseEntity
{
    // Property Details
    public string Name { get; set; }
    public string? Address { get; set; }
    public int TypeId { get; set; }
    public string? Image { get; set; }
    // Subscription Details
    public DateTime? StartSubscriptionDate { get; set; }
    public DateTime? EndSubscriptionDate { get; set; }
    public int? SubscriptionStatusId { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public int? TierId { get; set; }
    public int? WardId { get; set; }
    public int? ProvinceId { get; set; }
    // Nav
    public virtual List<User> Users { get; set; }
    public virtual List<UnitGroup> UnitGroups { get; set; }
    public virtual List<Service> Services { get; set; }
    public virtual List<Asset> Assets { get; set; }
    public virtual List<Job> Jobs { get; set; }
    public virtual CategoryItem Type {  get; set; }
    public virtual CategoryItem? SubscriptionStatus { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public virtual Tier? Tier { get; set; }
    public virtual Catalog.Ward? Ward { get; set; }
    public virtual Catalog.Province? Province { get; set; }
}