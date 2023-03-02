namespace DAL.DTOs
{
    public class ExpeditionReportDto
    {
        public bool IsSuccess { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Woods { get; set; }
        public int Stones { get; set; }
        public int Irons { get; set; }
        public int Coins { get; set; }
        public int Experience { get; set; }
    }
}
