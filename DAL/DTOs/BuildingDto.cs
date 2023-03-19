namespace DAL.DTOs
{
    public class BuildingDto
    {
        public int Id { get; set; }
        public string BuildingType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public string Description { get; set; } = string.Empty;
        public string SpritePath { get; set; } = string.Empty;
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
        public DateTime BuildDate { get; set; }
        public DateTime LastCollectDate { get; set; }
    }
}
