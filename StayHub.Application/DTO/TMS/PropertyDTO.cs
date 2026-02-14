using Shared.Common;
using StayHub.Application.DTO.Catalog;
using StayHub.Domain.Entity.Catalog;

namespace StayHub.Application.DTO.TMS;

public class PropertyDTO : BaseDTO
{
    public string Name { get; set; }
    public string? Address { get; set; }
    public CategoryItemDTO Type { get; set; }
    public string? Image { get; set; }
    // Subscription Details
    public DateTime? StartSubscriptionDate { get; set; }
    public DateTime? EndSubscriptionDate { get; set; }
    public CategoryItemDTO SubscriptionStatus { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public CategoryItemDTO Tier {  get; set; }
    public int? WardId { get; set; }
    public int? ProvinceId { get; set; }
    
}
