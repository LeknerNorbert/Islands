namespace Islands.DTOs
{
    public class NotificationDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsOpened { get; set; }
    }
}
