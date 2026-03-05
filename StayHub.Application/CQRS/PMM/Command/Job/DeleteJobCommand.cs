using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Job;
public record DeleteJobCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteJobCommandHandler(IJobRepository repository) : BaseResponseHandler, IRequestHandler<DeleteJobCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteJobCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.PMM.Job { Id = request.Id });
        return Success(true);
    }
}