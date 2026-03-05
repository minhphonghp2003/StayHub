using Shared.Common;
namespace StayHub.Application.DTO.PMM;
public class ServiceDTO : BaseDTO 
{ 
    public string Name { get; set; }
    public int FeeCategoryId { get; set; }
    public int TypeId { get; set; }
    public int VatTypeId { get; set; }
    public int PropertyId { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
}