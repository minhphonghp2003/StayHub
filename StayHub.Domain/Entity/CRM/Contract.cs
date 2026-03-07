using Shared.Common;
using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.PMM;
using StayHub.Domain.Entity.RBAC;
namespace StayHub.Domain.Entity.CRM;

public class Contract : BaseEntity
{
    public string Code { get; set; }
    public ContractStatus Status { get; set; }
    // Customer 
    public int UnitId { get; set; }
    public int VehicleNumber { get; set; }
    public bool IsSigned { get; set; }
    // Finance
    public long Price { get; set; }
    public long Deposit { get; set; }
    public long? DepositRemain { get; set; }
    public DateTime? DepositRemainEndDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PaymentPeriodId { get; set; }
    // Other
    public string? Note { get; set; }
    public string? Attachment { get; set; }
    public int? TemplateId { get; set; }
    public int? SaleId { get; set; }
    // Nav
    public virtual List<Customer> Customers { get; set; }
    public virtual List<ContractService>? ContractServices { get; set; }
    public virtual List<ContractAsset>? ContractAssets { get; set; }
    public virtual CategoryItem PaymentPeriod { get; set; }
    public virtual Unit Unit { get; set; }
    public virtual User? Sale { get; set; }
}