using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.HRM;
using StayHub.Application.Interfaces.Repository.HRM;

namespace StayHub.Application.CQRS.HRM.Query.Employee;

public record GetAllEmployeeQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<EmployeeDTO>>;

public sealed class GetAllEmployeeQueryHandler(IEmployeeRepository repository, IConfiguration configuration) 
    : BaseResponseHandler, IRequestHandler<GetAllEmployeeQuery, Response<EmployeeDTO>>
{
    public async Task<Response<EmployeeDTO>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.pageSize ?? configuration.GetValue<int>("PageSize");
        var (result, count) = await repository.GetAllEmployeePaginated(request.pageNumber ?? 1, pageSize, request.searchKey);
        return SuccessPaginated(result, count, pageSize, request.pageNumber ?? 1);
    }
}
