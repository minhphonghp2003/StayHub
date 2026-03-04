using StayHub.Application.Interfaces.Repository.PMM;
using Asset = StayHub.Domain.Entity.PMM.Asset;
namespace StayHub.Infrastructure.Persistence.Repository.PMM;
public class AssetRepository(AppDbContext context) 
    : PagingAndSortingRepository<Asset>(context), IAssetRepository { }