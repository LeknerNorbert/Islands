namespace Islands.DTOs
{
    public class BuildingDefaultValuesDTO
    {
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public int CoinsForUpdate { get; set; }
        public int IronsForUpdate { get; set; }
        public int StonesForUpdate { get; set; }
        public int WoodsForUpdate { get; set; }
        public int ProductionInterval { get; set; }
        public int ProducedCoins { get; set; }
        public int ProducedIrons { get; set; }
        public int ProducedStones { get; set; }
        public int ProducedWoods { get; set; }
        public int ExperienceReward { get; set; }
    }
}
