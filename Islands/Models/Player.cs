using Islands.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Islands.Models
{
    public class Player
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ForeignKey("User")]
        public int Id { get; set; }
        public int Experience { get; set; }
        public int Coins { get; set; }
        public int Woods { get; set; }
        public int Stones { get; set; }
        public int Irons { get; set; }
        public IslandType SelectedIsland { get; set; }
        public DateTime LastExpeditionDate { get; set; }
        public DateTime LastBattleDate { get; set; }
        [Range(0, 30)]
        public int Strength { get; set; }
        [Range(0, 30)]
        public int Intelligence { get; set; }
        [Range(0, 30)]
        public int Agility { get; set; }
        public virtual User? User { get; set; }
        public ICollection<Building>? Buildings { get; set; }
        public ICollection<Exchange>? Exchanges { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
    }
}
