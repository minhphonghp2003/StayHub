using System.Net;
using MediatR;
using Shared.Response;
using StayHub.Application.DTO.HRM;
using StayHub.Application.Interfaces.Repository.HRM;

namespace StayHub.Application.CQRS.HRM.Query.Employee;

public record GetEmployeeByIdQuery(int Id) : IRequest<BaseResponse<EmployeeDTO>>;

internal sealed class GetEmployeeByIdQueryHandler(IEmployeeRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetEmployeeByIdQuery, BaseResponse<EmployeeDTO>>
{
    public async Task<BaseResponse<EmployeeDTO>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.FindOneAsync(e => e.Id == request.Id, e => new EmployeeDTO { Id = e.Id });
        return result == null ? Failure<EmployeeDTO>("Not found",HttpStatusCode.BadRequest) : Success(result);
    }
}
