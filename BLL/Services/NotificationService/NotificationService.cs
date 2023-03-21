using DAL.DTOs;
using DAL.Models;
using DAL.Repositories.NotificationRepository;

namespace BLL.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<NotificationDto>> GetAllNotificationsByUsernameAsync(string username)
        {
            return await _notificationRepository.GetAllNotificationsByUsername(username);
        }

        public async Task RemoveNotificationAsync(int id)
        {
            Notification removedNotification = await _notificationRepository.GetNotificationById(id);
            await _notificationRepository.RemoveNotification(removedNotification);
        }
    }
}
