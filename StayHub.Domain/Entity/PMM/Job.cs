using Shared.Common;
namespace StayHub.Domain.Entity.PMM;
public class Job : BaseEntity 
{ 
    public string Name { get; set; }
    public int PropertyId { get; set; }
    public int UnitId { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}