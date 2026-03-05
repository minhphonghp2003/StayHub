using Shared.Common;
namespace StayHub.Application.DTO.CRM;
public class ContractDTO : BaseDTO 
{ 
    public int RoomId { get; set; }
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