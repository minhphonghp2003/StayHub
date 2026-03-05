using Shared.Common;
namespace StayHub.Application.DTO.CRM;
public class ContractServiceDTO : BaseDTO 
{ 
    public int ContractId { get; set; }
    public int ServiceId { get; set; }
    public string Value { get; set; }
}