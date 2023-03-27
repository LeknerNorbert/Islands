namespace DAL.DTOs
{
    public class EnemyDto
    {
        public string Username { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public string SpritePath { get; set; } = string.Empty;
        public int Level { get; set; }
        public int Health { get; set; }
    }
}
