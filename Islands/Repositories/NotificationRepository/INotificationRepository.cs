using Islands.Models;

namespace Islands.Repositories.NotificationRepository
{
    public interface INotificationRepository
    {
        Task AddNotification(Notification notification);
    }
}
