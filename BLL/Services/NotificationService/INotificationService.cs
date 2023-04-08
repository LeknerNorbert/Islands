using DAL.DTOs;
using DAL.Models;

namespace BLL.Services.NotificationService
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetAllNotificationsByUsernameAsync(string username);
        Task RemoveNotificationAsync(int id);
        Task AddNotificationAsync (Notification notification, string receiverUsername);
        Task SetNotificationToOpenedAsync(int id);
    }
}