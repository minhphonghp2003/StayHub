using Shared.Common;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.DTO.CRM;
using StayHub.Domain.Entity.Catalog;
namespace StayHub.Application.DTO.FMS;
public class InOutComeDTO : BaseDTO 
{ 
    public long Amount { get; set; }
    public int PaymentMethodId { get; set; }
    public string Payer { get; set; }
    public string Description { get; set; }
    public int TypeId { get; set; }
    public DateTime Date { get; set; }
    public int? ContractId { get; set; }
    public bool IsRepeatMonthly { get; set; }
    public bool IsOutCome { get; set; }
    public CategoryItemDTO PaymentMethod { get; set; }
    public CategoryItemDTO Type{ get; set; }
    public ContractDTO?  Contract    { get; set; }
}