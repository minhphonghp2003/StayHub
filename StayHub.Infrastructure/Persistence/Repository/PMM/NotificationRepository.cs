using StayHub.Application.Interfaces.Repository.PMM;
using Notification = StayHub.Domain.Entity.PMM.Notification;
namespace StayHub.Infrastructure.Persistence.Repository.PMM;
public class NotificationRepository(AppDbContext context) : PagingAndSortingRepository<Notification>(context), INotificationRepository { }