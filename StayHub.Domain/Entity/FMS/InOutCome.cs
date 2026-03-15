using Shared.Common;
using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.CRM;
using StayHub.Domain.Entity.PMM;
namespace StayHub.Domain.Entity.FMS;
public class InOutCome : BaseEntity 
{
    public int PropertyId { get; set; }
    public long Amount { get; set; }
    public int PaymentMethodId { get; set; }
    public string Payer { get; set; }
    public string Description { get; set; }
    public int TypeId { get; set; }
    public DateTime Date { get; set; }
    public int? ContractId { get; set; }
    public bool IsRepeatMonthly { get; set; }
    public bool IsOutCome { get; set; }
    //Nav
    public virtual CategoryItem PaymentMethod { get; set; }
    public virtual Property  Property { get; set; }

    public virtual CategoryItem Type { get; set; }
    public virtual Contract? Contract { get; set; }
}