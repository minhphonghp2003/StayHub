using Shared.Common;
namespace StayHub.Domain.Entity.CRM;
public class ContractAsset : BaseEntity 
{ 
    public int ContractId { get; set; }
    public int AssetId { get; set; }
    public int Quantity { get; set; }
}