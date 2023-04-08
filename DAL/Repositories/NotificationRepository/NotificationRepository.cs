using DAL.DTOs;
using DAL.Models;
using DAL.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.NotificationRepository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;
        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Notification> AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await _context.Notifications
                .Include(notification => notification.Player)
                      .ThenInclude(player => player.User)
                .FirstAsync(notification => notification.Id == id);
        }

        public async Task<List<NotificationDto>> GetAllNotificationsByUsernameAsync(string username)
        {
            return await _context.Notifications
                .Include(notification => notification.Player)
                    .ThenInclude(player => player.User)
                .Where(notification => notification.Player.User.Username == username)
                .Select(notification => new NotificationDto()
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
                })
                .OrderByDescending(notification => notification.CreateDate)
                .ToListAsync();
        }

        public async Task RemoveNotificationAsync(Notification notification)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            _context.Entry(notification).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
