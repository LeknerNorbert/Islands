using DAL.DTOs;

namespace BLL.Services.NotificationService
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetAllNotificationsByUsernameAsync(string username);
        Task RemoveNotificationAsync(int id);
    }
}
