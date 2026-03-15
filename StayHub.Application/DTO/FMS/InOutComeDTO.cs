using Shared.Common;
namespace StayHub.Application.DTO.FMS;
public class InOutComeDTO : BaseDTO 
{ 
    public long Amount { get; set; }
    public int PaymentMethod { get; set; }
    public string Payer { get; set; }
    public string Description { get; set; }
    public int TypeId { get; set; }
    public DateTime Date { get; set; }
    public int? ContractId { get; set; }
    public bool IsRepeatMonthly { get; set; }
    public bool IsOutCome { get; set; }
}