using DAL.DTOs;
using DAL.Models;

namespace DAL.Repositories.NotificationRepository
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotificationAsync(Notification notification);
        Task<Notification> GetNotificationByIdAsync(int id);
        Task<List<NotificationDto>> GetAllNotificationsByUsernameAsync(string username);
        Task RemoveNotificationAsync(Notification notification);
        Task UpdateNotificationAsync(Notification notification);
    }
}
