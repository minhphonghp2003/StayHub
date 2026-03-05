using StayHub.Application.Interfaces.Repository.CRM;
using Contract = StayHub.Domain.Entity.CRM.Contract;
namespace StayHub.Infrastructure.Persistence.Repository.CRM;
public class ContractRepository(AppDbContext context) : PagingAndSortingRepository<Contract>(context), IContractRepository { }