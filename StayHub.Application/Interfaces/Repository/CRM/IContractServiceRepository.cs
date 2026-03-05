using ContractService = StayHub.Domain.Entity.CRM.ContractService;
namespace StayHub.Application.Interfaces.Repository.CRM;
public interface IContractServiceRepository : IPagingAndSortingRepository<ContractService> { }