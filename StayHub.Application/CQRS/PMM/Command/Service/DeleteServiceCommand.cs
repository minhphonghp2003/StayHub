using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Service;
public record DeleteServiceCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteServiceCommandHandler(IServiceRepository repository) : BaseResponseHandler, IRequestHandler<DeleteServiceCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteServiceCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.PMM.Service { Id = request.Id });
        return Success(true);
    }
}