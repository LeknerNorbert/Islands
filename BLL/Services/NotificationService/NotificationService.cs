using BLL.Services.AuthService;
using DAL.DTOs;
using DAL.Models;
using DAL.Repositories.NotificationRepository;
using Microsoft.AspNetCore.SignalR.Client;

namespace BLL.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IAuthService _authService;
        private readonly HubConnection _notificationHubConnection;

        public NotificationService(INotificationRepository notificationRepository, IAuthService authService)
        {

            _notificationRepository = notificationRepository;
            _authService = authService;
            _notificationHubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7276/notificationHub", options =>
                {
                    options.Headers.Add("Authorization", "Bearer " + _authService.GetTokenForNotificationHub());
                })
                .Build();
        }

        public async Task<List<NotificationDto>> GetAllNotificationsByUsernameAsync(string username)
        {
            return await _notificationRepository.GetAllNotificationsByUsernameAsync(username);
        }

        public async Task RemoveNotificationAsync(int id)
        {
            Notification removedNotification = await _notificationRepository.GetNotificationByIdAsync(id);
            await _notificationRepository.RemoveNotificationAsync(removedNotification);
        }

        public async Task AddNotificationAsync(Notification notification, string receiverUsername)
        {
            Notification addedNotification = await _notificationRepository.AddNotificationAsync(notification);
            await SendNotificationToClientAsync(addedNotification, receiverUsername);
        }

        public async Task SetNotificationToOpenedAsync(int id)
        {
            Notification notification = await _notificationRepository.GetNotificationByIdAsync(id);
            notification.IsOpened = true;
            
            await _notificationRepository.UpdateNotificationAsync(notification);
        }

        private async Task SendNotificationToClientAsync(Notification notification, string receiverUsername)
        {
            NotificationDto notificationToClient = new()
            {
                Id = notification.Id,
                Title = notification.Title,
                Message = notification.Message,
                Woods = notification.Woods,
                Stones = notification.Stones,
                Irons = notification.Irons,
                Coins = notification.Coins,
                Experience = notification.Experience,
                IsOpened = notification.IsOpened,
                CreateDate = notification.CreateDate,
            };

            await _notificationHubConnection.StartAsync();
            await _notificationHubConnection.InvokeAsync("SendNotification", notificationToClient, receiverUsername);
            await _notificationHubConnection.StopAsync();
        }
    }
}
