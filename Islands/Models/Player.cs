using Islands.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Islands.Models
{
    public class Player
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ForeignKey("User")]
        public int Id { get; set; }
        public int ExperiencePoint { get; set; }
        public int Coins { get; set; }
        public int Woods { get; set; }
        public int Stones { get; set; }
        public int Irons { get; set; }
        public IslandType SelectedIsland { get; set; }
        public DateTime LastExpeditionDate { get; set; }
        public DateTime LastBattleDate { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Ability { get; set; }
        public virtual User? User { get; set; }
        public ICollection<Building>? Buildings { get; set; }
        public ICollection<ClassifiedAd>? ClassifiedAds { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
    }
}
