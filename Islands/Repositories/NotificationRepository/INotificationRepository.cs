using Islands.Models;

namespace Islands.Repositories.NotificationRepository
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
    }
}
