<<<<<<<< HEAD:DAL/DTOs/BuildingConfigurationDto.cs
﻿namespace DAL.DTOs
{
    public class BuildingConfigurationDto
    {
        public string Description { get; set; } = string.Empty;
        public string SpritePath { get; set; } = string.Empty;
========
﻿namespace Islands.DTOs
{
    public class BuildingWithDefaultValuesDto
    {
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
>>>>>>>> master:Islands/Models/DTOs/BuildingWithDefaultValuesDTO.cs
        public int MaxLevel { get; set; }
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
        public int BuildTime { get; set; }
    }
}
