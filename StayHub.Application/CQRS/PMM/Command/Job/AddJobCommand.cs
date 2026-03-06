using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Job;
public record AddJobCommand(string Name, int PropertyId, int? UnitId, string Description, bool IsActive) : IRequest<BaseResponse<bool>>;
public sealed class AddJobCommandHandler(IJobRepository repository) : BaseResponseHandler, IRequestHandler<AddJobCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddJobCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.PMM.Job 
        { 
            Name = request.Name,
            PropertyId = request.PropertyId,
            UnitId = request.UnitId,
            Description = request.Description,
            IsActive = request.IsActive
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}