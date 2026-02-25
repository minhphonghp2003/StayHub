using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.HRM;

namespace StayHub.Application.CQRS.HRM.Command.Employee;

public record DeleteEmployeeCommand(int Id) : IRequest<BaseResponse<bool>>;

public sealed class DeleteEmployeeCommandHandler(IEmployeeRepository repository) 
    : BaseResponseHandler, IRequestHandler<DeleteEmployeeCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(new Domain.Entity.HRM.Employee { Id = request.Id });
        return Success(true);
    }
}
