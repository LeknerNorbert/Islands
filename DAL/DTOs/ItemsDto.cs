namespace DAL.DTOs
{
    public class ItemsDto
    {
        public int Coins { get; set; }
        public int Woods { get; set; }
        public int Stones { get; set; }
        public int Irons { get; set; }

        public ItemsDto(int coins, int woods, int stones, int irons)
        {
            Coins = coins;
            Woods = woods;
            Stones = stones;
            Irons = irons;
        }
    }
}
