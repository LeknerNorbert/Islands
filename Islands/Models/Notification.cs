using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Islands.Models
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool IsOpened { get; set; }
        public Player? PlayerInformation { get; set; }
    }
}
