using MediatR;
using Shared.Response;
using StayHub.Application.DTO.HRM;
using StayHub.Application.Interfaces.Repository.HRM;

namespace StayHub.Application.CQRS.HRM.Command.Employee;

public record AddEmployeeCommand() : IRequest<BaseResponse<bool>>;

public sealed class AddEmployeeCommandHandler(IEmployeeRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddEmployeeCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entity.HRM.Employee { };
        await repository.AddAsync(entity);
        return Success(true);
    }
}
