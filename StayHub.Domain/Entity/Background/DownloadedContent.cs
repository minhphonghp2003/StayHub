using Shared.Common;
namespace StayHub.Domain.Entity.Background;
public class DownloadedContent : BaseEntity 
{ 
    public string Name { get; set; }
    public string Url { get; set; }
}