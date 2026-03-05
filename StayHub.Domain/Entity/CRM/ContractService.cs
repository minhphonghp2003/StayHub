using Shared.Common;
namespace StayHub.Domain.Entity.CRM;
public class ContractService : BaseEntity 
{ 
    public int ContractId { get; set; }
    public int ServiceId { get; set; }
    public int Value { get; set; }
}