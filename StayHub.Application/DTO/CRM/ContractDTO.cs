using Shared.Common;
using StayHub.Application.DTO.PMM;
namespace StayHub.Application.DTO.CRM;
public class ContractDTO : BaseDTO 
{ 
    public int UnitId { get; set; }
    public UnitDTO Unit { get; set; }
    public CustomerDTO Customer { get; set; }
    public String Status { get; set; }
    public long Price { get; set; }
    public long Deposit { get; set; }
    public long? DepositRemain { get; set; }
    public DateTime? DepositRemainEndDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PaymentPeriodId { get; set; }
    public string? Note { get; set; }
    public string? Attachment { get; set; }
    public string Code { get; set; }
    public bool IsSigned { get; set; }
    public int? TemplateId { get; set; }
}