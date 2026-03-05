using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Notification;
public record AddNotificationCommand(string Title, string Body, int? PayloadId, string? PayloadType, string? Icon, string? Avatar, bool IsRead, int? UnitId, int OwnerId) : IRequest<BaseResponse<bool>>;
public sealed class AddNotificationCommandHandler(INotificationRepository repository) : BaseResponseHandler, IRequestHandler<AddNotificationCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddNotificationCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.PMM.Notification 
        { 
            Title = request.Title,
            Body = request.Body,
            PayloadId = request.PayloadId,
            PayloadType = request.PayloadType,
            Icon = request.Icon,
            Avatar = request.Avatar,
            IsRead = request.IsRead,
            UnitId = request.UnitId,
            OwnerId = request.OwnerId
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}