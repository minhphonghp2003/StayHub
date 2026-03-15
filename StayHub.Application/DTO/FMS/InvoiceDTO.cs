using Shared.Common;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.DTO.PMM;
using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.PMM;
namespace StayHub.Application.DTO.FMS;
public class InvoiceDTO : BaseDTO 
{ 
    public UnitDTO  Unit { get; set; }
    public int UnitId { get; set; }
    public CategoryItemDTO Reason { get; set; }
    public int ReasonId { get; set; }
    public DateTime Month { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string Status { get; set; }
    public List<InvoiceServiceDTO>? Services { get; set; }
    public string? Note { get; set; }
    public long? Discount { get; set; }
}