using Shared.Common;
using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.PMM;
namespace StayHub.Domain.Entity.FMS;

public class Invoice : BaseEntity
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
    public double RemainAmount { get; set; }
    public InvoiceStatus Status { get; set; }
    //Nav 
    public virtual Unit Unit { get; set; }
    public virtual CategoryItem Reason { get; set; }
    public virtual List<InvoiceService>? Services { get; set; }

}