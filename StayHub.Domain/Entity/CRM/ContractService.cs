using Shared.Common;
using StayHub.Domain.Entity.PMM;
namespace StayHub.Domain.Entity.CRM;
public class ContractService : BaseEntity 
{ 
    public int ContractId { get; set; }
    public int ServiceId { get; set; }
    public string Value { get; set; }
    //Nav
    public virtual Contract Contract { get; set; }
    public virtual Service Service { get; set; }
}