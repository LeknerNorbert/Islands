namespace DAL.DTOs
{
    public class PlayerForBattleDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Experience { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int ChurchLevel { get; set; }
        public int PracticeRangeLevel { get; set; }
        public DateTime LastBattleDate { get; set; }
    }
}
