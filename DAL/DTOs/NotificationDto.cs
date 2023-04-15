namespace DAL.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Woods { get; set; }
        public int Stones { get; set; }
        public int Irons { get; set; }
        public int Coins { get; set; }
        public int Experience { get; set; }
        public bool IsOpened { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsItemsUpdateForce { get; set; }
    }
}
