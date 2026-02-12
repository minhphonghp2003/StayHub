namespace StayHub.Domain.Entity.TMS;

public class Tier :BaseEntity
{
    
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public int Price { get; set; }
    public string BillingCycle { get; set; }
    public virtual List<RBAC.Action>? Actions { get; set; }
}