using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Notification;
public record DeleteNotificationCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteNotificationCommandHandler(INotificationRepository repository) : BaseResponseHandler, IRequestHandler<DeleteNotificationCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteNotificationCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.PMM.Notification { Id = request.Id });
        return Success(true);
    }
}