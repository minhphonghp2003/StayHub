using Shared.Common;
namespace StayHub.Application.DTO.PMM;
public class JobDTO : BaseDTO 
{ 
    public string Name { get; set; }
    public PropertyDTO Property { get; set; }
    public UnitDTO? Unit { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}