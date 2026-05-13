using DownloadedContent = StayHub.Domain.Entity.Background.DownloadedContent;
namespace StayHub.Application.Interfaces.Repository.Background;
public interface IDownloadedContentRepository : IPagingAndSortingRepository<DownloadedContent> { }