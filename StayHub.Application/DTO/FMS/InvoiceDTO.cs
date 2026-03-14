using Shared.Common;
namespace StayHub.Application.DTO.FMS;
public class InvoiceDTO : BaseDTO 
{ 
    public int UnitId { get; set; }
    public int ReasonId { get; set; }
    public DateTime Month { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? Note { get; set; }
    public long? Discount { get; set; }
}