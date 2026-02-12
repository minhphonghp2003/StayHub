using Shared.Common;

namespace StayHub.Domain.Entity.TMS;

public class Property: BaseEntity
{
    // Property Details
    public string Name { get; set; }
    public string? Address { get; set; }
    public PropertyType Type { get; set; }
    public string? Image { get; set; }
    // Subscription Details
    public DateTime? StartSubscriptionDate { get; set; }
    public DateTime? EndSubscriptionDate { get; set; }
    public SubscriptionStatus SubscriptionStatus { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public int TierId { get; set; }
    public int? WardId { get; set; }
    public int? ProvinceId { get; set; }
    // Nav
    public virtual Tier Tier { get; set; }
    public virtual Catalog.Ward? Ward { get; set; }
    public virtual Catalog.Province? Province { get; set; }
}