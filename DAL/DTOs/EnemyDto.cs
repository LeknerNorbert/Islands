namespace DAL.DTOs
{
    public class EnemyDto
    {
        public int PlayerId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string SpritePath { get; set; } = string.Empty;
        public int Level { get; set; }
    }
}
