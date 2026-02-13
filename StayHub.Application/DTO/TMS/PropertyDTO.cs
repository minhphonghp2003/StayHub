using Shared.Common;

namespace StayHub.Application.DTO.TMS;

public class PropertyDTO : BaseDTO
{
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
    
}
