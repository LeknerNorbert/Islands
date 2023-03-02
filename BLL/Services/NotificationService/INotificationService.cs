using DAL.DTOs;

namespace BLL.Services.NotificationService
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetAllNotificationByUsernameAsync(string username);
        Task RemoveNotificationAsync(int id);
    }
}
