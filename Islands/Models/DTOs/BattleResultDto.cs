namespace Islands.Models.DTOs
{
    public class BattleResultDto
    {
        public bool IsWinner { get; set; }
        public int Woods { get; set; }
        public int Stones { get; set; }
        public int Irons { get; set; }
        public int Coins { get; set; }
        public int ExperiencePoints { get; set; }
        public List<BattleReportDto> Reports { get; set; }

        public BattleResultDto()
        {
            Reports = new List<BattleReportDto>();
        }
    }
}
