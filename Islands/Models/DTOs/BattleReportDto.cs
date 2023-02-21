namespace Islands.Models.DTOs
{
    public class BattleReportDto
    {
        public string Username { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Damage { get; set; }
        public int EnemyReminingHealth { get; set; }
    }
}
