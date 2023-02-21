using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Islands.Models
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Wood { get; set; }
        public int Stone { get; set; }
        public int Iron { get; set; }
        public int Coin { get; set; }
        public int ExperiencePoint { get; set; }
        public DateTime Date { get; set; }
        public Player? Player { get; set; }
    }
}
