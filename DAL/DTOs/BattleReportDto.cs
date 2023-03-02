namespace DAL.DTOs
{
    public class BattleReportDto
    {
        public bool IsWinner { get; set; }
        public int Woods { get; set; }
        public int Stones { get; set; }
        public int Irons { get; set; }
        public int Coins { get; set; }
        public int Experience { get; set; }
        public List<RoundDto> Rounds { get; set; }

        public BattleReportDto()
        {
            Rounds = new List<RoundDto>();
        }
    }
}
