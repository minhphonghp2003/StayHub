using MediatR;
using Shared.Response;
using StayHub.Application.DTO.HRM;
using StayHub.Application.Interfaces.Repository.HRM;

namespace StayHub.Application.CQRS.HRM.Query.Employee;

public record GetAllEmployeeNoPaginatedQuery() : IRequest<BaseResponse<List<EmployeeDTO>>>;

public class GetAllEmployeeNoPaginatedQueryHandler(IEmployeeRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetAllEmployeeNoPaginatedQuery, BaseResponse<List<EmployeeDTO>>>
{
    public async Task<BaseResponse<List<EmployeeDTO>>> Handle(GetAllEmployeeNoPaginatedQuery request, CancellationToken cancellationToken)
    {
        var data = await repository.GetAllEmployeeNoPaginated();
        return Success(data);
    }
}
