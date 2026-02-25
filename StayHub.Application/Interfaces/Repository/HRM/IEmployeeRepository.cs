using StayHub.Domain.Entity.HRM;
using StayHub.Application.Interfaces.Repository;
using StayHub.Application.DTO.HRM;

namespace StayHub.Application.Interfaces.Repository.HRM;

public interface IEmployeeRepository : IPagingAndSortingRepository<Employee>
{
    Task<(List<EmployeeDTO>, int)> GetAllEmployeePaginated(int pageNumber, int pageSize, string? searchKey);
    Task<List<EmployeeDTO>> GetAllEmployeeNoPaginated();
}
