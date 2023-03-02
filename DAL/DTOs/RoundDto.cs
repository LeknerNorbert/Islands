namespace DAL.DTOs
{
    public class RoundDto
    {
        public string Username { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Damage { get; set; }
        public int EnemyReminingHealth { get; set; }
    }
}
