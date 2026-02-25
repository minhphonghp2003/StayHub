using StayHub.Domain.Entity.HRM;
using StayHub.Application.DTO.HRM;
using StayHub.Application.Interfaces.Repository.HRM;
using StayHub.Infrastructure.Persistence.Repository;

namespace StayHub.Infrastructure.Persistence.Repository.HRM;

public class EmployeeRepository(AppDbContext context) 
    : PagingAndSortingRepository<Employee>(context), IEmployeeRepository
{
    public async Task<(List<EmployeeDTO>, int)> GetAllEmployeePaginated(int pageNumber, int pageSize, string? searchKey)
    {
        return await GetManyPagedAsync(
            filter: x => string.IsNullOrEmpty(searchKey) || x.Id.ToString().Contains(searchKey),
            selector: (x, i) => new EmployeeDTO { Id = x.Id },
            pageNumber: pageNumber,
            pageSize: pageSize
        );
    }

    public async Task<List<EmployeeDTO>> GetAllEmployeeNoPaginated()
    {
        var result = await GetAllAsync((x, i) => new EmployeeDTO { Id = x.Id });
        return result.ToList();
    }
}
