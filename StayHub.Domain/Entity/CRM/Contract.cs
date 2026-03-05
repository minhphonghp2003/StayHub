using Shared.Common;
using StayHub.Domain.Entity.Catalog;
using StayHub.Domain.Entity.PMM;
using StayHub.Domain.Entity.RBAC;
namespace StayHub.Domain.Entity.CRM;

public class Contract : BaseEntity
{
    public int UnitId { get; set; }
    public int? SaleId { get; set; }
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
    public ContractStatus Status { get; set; }
    // Nav
    public virtual List<User> Customers { get; set; }
    public virtual List<Service> Services { get; set; }
    public virtual List<ContractAsset> ContractAssets { get; set; }
    public virtual CategoryItem PaymentPeriod { get; set; }
    public virtual Unit Unit { get; set; }
    public virtual User? Sale { get; set; }
}