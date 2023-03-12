using Islands.Models;
using Islands.Models.Context;

namespace Islands.Repositories.NotificationRepository
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly ApplicationDbContext context;
        public NotificationRepository(ApplicationDbContext context) 
        {
            this.context = context;
        }
        public async Task AddNotificationAsync(Notification notification)
        {
            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
        }



    }
}
