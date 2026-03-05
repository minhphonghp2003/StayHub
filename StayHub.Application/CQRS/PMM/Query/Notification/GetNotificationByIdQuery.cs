using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Query.Notification;
public record GetNotificationByIdQuery(int Id) : IRequest<BaseResponse<NotificationDTO>>;
public sealed class GetNotificationByIdQueryHandler(INotificationRepository repository) : BaseResponseHandler, IRequestHandler<GetNotificationByIdQuery, BaseResponse<NotificationDTO>> 
{
    public async Task<BaseResponse<NotificationDTO>> Handle(GetNotificationByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new NotificationDTO 
        { 
            Id = x.Id, 
            Title = x.Title,
            Body = x.Body,
            PayloadId = x.PayloadId,
            PayloadType = x.PayloadType,
            Icon = x.Icon,
            Avatar = x.Avatar,
            IsRead = x.IsRead,
            UnitId = x.UnitId,
            OwnerId = x.OwnerId
        });
        return result == null ? Failure<NotificationDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}