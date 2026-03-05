using Shared.Common;
namespace StayHub.Application.DTO.CRM;
public class ContractAssetDTO : BaseDTO 
{ 
    public int ContractId { get; set; }
    public int AssetId { get; set; }
    public int Quantity { get; set; }
}