using StayHub.Application.Interfaces.Repository.CRM;
using ContractAsset = StayHub.Domain.Entity.CRM.ContractAsset;
namespace StayHub.Infrastructure.Persistence.Repository.CRM;
public class ContractAssetRepository(AppDbContext context) : PagingAndSortingRepository<ContractAsset>(context), IContractAssetRepository { }