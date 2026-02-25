using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.HRM;
using StayHub.Application.Interfaces.Repository.HRM;

namespace StayHub.Application.CQRS.HRM.Command.Employee;

public class UpdateEmployeeCommand : IRequest<BaseResponse<EmployeeDTO>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public UpdateEmployeeCommand(){}
}

public sealed class UpdateEmployeeCommandHandler(IEmployeeRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateEmployeeCommand, BaseResponse<EmployeeDTO>>
{
    public async Task<BaseResponse<EmployeeDTO>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<EmployeeDTO>("Not found", HttpStatusCode.BadRequest);
        
        repository.Update(entity);
        return Success(new EmployeeDTO { Id = entity.Id });
    }
}
