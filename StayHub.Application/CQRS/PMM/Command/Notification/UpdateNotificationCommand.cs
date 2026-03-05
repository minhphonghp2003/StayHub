using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Notification;
public class UpdateNotificationCommand : IRequest<BaseResponse<NotificationDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int? PayloadId { get; set; }
    public string? PayloadType { get; set; }
    public string? Icon { get; set; }
    public string? Avatar { get; set; }
    public bool IsRead { get; set; }
    public int? UnitId { get; set; }
    public int OwnerId { get; set; }
}
public sealed class UpdateNotificationCommandHandler(INotificationRepository repository) : BaseResponseHandler, IRequestHandler<UpdateNotificationCommand, BaseResponse<NotificationDTO>> 
{
    public async Task<BaseResponse<NotificationDTO>> Handle(UpdateNotificationCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<NotificationDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.Title = request.Title;
        entity.Body = request.Body;
        entity.PayloadId = request.PayloadId;
        entity.PayloadType = request.PayloadType;
        entity.Icon = request.Icon;
        entity.Avatar = request.Avatar;
        entity.IsRead = request.IsRead;
        entity.UnitId = request.UnitId;
        entity.OwnerId = request.OwnerId;

        repository.Update(entity);
        return Success(new NotificationDTO 
        { 
            Id = entity.Id, 
            Title = entity.Title,
            Body = entity.Body,
            PayloadId = entity.PayloadId,
            PayloadType = entity.PayloadType,
            Icon = entity.Icon,
            Avatar = entity.Avatar,
            IsRead = entity.IsRead,
            UnitId = entity.UnitId,
            OwnerId = entity.OwnerId
        });
    }
}