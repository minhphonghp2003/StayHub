using StayHub.Application.Interfaces.Repository.Background;
using DownloadedContent = StayHub.Domain.Entity.Background.DownloadedContent;
namespace StayHub.Infrastructure.Persistence.Repository.Background;
public class DownloadedContentRepository(AppDbContext context) : PagingAndSortingRepository<DownloadedContent>(context), IDownloadedContentRepository { }