using StayHub.Application.Interfaces.Repository.CRM;
using ContractService = StayHub.Domain.Entity.CRM.ContractService;
namespace StayHub.Infrastructure.Persistence.Repository.CRM;
public class ContractServiceRepository(AppDbContext context) : PagingAndSortingRepository<ContractService>(context), IContractServiceRepository { }