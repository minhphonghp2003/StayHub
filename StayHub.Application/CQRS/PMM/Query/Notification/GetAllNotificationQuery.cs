using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Query.Notification;
public record GetAllNotificationQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<NotificationDTO>>;
public sealed class GetAllNotificationQueryHandler(INotificationRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllNotificationQuery, Response<NotificationDTO>> 
{
    public async Task<Response<NotificationDTO>> Handle(GetAllNotificationQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => request.searchKey == null,
            selector: (x, i) => new NotificationDTO 
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
            }
        );
        return SuccessPaginated(result.ToList(), count, request.pageNumber ?? 1, size);
    }
}