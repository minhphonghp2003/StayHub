using Shared.Common;
using StayHub.Domain.Entity.PMM;
namespace StayHub.Application.DTO.PMM;
public class UnitDTO : BaseDTO 
{ 
    public string Name { get; set; } = string.Empty;
    public UnitStatus Status { get; set; }
    public decimal BasePrice { get; set; }
    public int MaximumCustomer { get; set; }
    public bool IsActive { get; set; }

    public  UnitGroupDTO UnitGroup { get; set; }
}