using Islands.Models.Enums;

namespace Islands.DTOs
{
    public class PlayerDto
    {
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
        public int Agility { get; set; }
    }
}
