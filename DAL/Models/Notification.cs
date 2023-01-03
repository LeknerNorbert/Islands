using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool IsOpened { get; set; }
        public PlayerInformation? PlayerInformation { get; set; }
    }
}
