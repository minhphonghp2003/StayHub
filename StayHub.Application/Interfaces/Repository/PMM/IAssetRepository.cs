using Asset = StayHub.Domain.Entity.PMM.Asset;
namespace StayHub.Application.Interfaces.Repository.PMM;
public interface IAssetRepository : IPagingAndSortingRepository<Asset> { }