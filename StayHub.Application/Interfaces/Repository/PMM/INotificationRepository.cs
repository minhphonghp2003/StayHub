using Notification = StayHub.Domain.Entity.PMM.Notification;
namespace StayHub.Application.Interfaces.Repository.PMM;
public interface INotificationRepository : IPagingAndSortingRepository<Notification> { }