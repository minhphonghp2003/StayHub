using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.FMS;
namespace StayHub.Application.CQRS.FMS.Command.InOutCome;
public record DeleteInOutComeCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteInOutComeCommandHandler(IInOutComeRepository repository) : BaseResponseHandler, IRequestHandler<DeleteInOutComeCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteInOutComeCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.FMS.InOutCome { Id = request.Id });
        return Success(true);
    }
}