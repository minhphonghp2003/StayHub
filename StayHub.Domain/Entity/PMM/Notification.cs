using Shared.Common;
namespace StayHub.Domain.Entity.PMM;
public class Notification : BaseEntity 
{ 
    public string Title { get; set; }
    public string Body { get; set; }
    public int? PayloadId { get; set; }
    public string? PayloadType { get; set; }
    public string? Icon { get; set; }
    public string? Avatar { get; set; }
    public bool IsRead { get; set; }
    public int? UnitId { get; set; }
    public int OwnerId { get; set; }
}