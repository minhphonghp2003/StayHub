namespace StayHub.Application.DTO.TMS;

public class TierDTO : BaseDTO
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string BillingCycle { get; set; }
}
