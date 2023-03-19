using DAL.Models.Enums;

namespace DAL.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public int Experience { get; set; }
        public int Coins { get; set; }
        public int Woods { get; set; }
        public int Stones { get; set; }
        public int Irons { get; set; }
        public string SelectedIsland { get; set; } = string.Empty;
        public string ProfileImagePath { get; set; } = string.Empty;
        public DateTime LastExpeditionDate { get; set; }
        public DateTime LastBattleDate { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
    }
}
