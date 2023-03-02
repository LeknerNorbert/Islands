using DAL.DTOs;
using DAL.Models;

namespace DAL.Repositories.NotificationRepository
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
        Task<Notification> GetNotificationById(int id);
        Task<List<NotificationDto>> GetAllNotificationByUsername(string username);
        Task RemoveNotification(Notification notification);
    }
}
