using DAL.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationHub : Hub
    {
        public async Task SendNotification(NotificationDto notification, string receiverUsername)
        {
            await Clients.User(receiverUsername).SendAsync("ReceiveNotification", notification);
        }
    }
}