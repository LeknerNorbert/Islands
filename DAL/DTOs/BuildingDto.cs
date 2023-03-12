<<<<<<<< HEAD:DAL/DTOs/BuildingDto.cs
﻿namespace DAL.DTOs
{
    public class BuildingDto
    {
        public string Name { get; set; } = string.Empty;
========
﻿using Islands.Models.Enums;

namespace Islands.DTOs
{
    public class BuildingDto
    {
        public BuildingType Name { get; set; }
>>>>>>>> master:Islands/Models/DTOs/BuildingDTO.cs
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public string Description { get; set; } = string.Empty;
<<<<<<<< HEAD:DAL/DTOs/BuildingDto.cs
        public string SpritePath { get; set; } = string.Empty;
========
        public string ImagePath { get; set; } = string.Empty;
>>>>>>>> master:Islands/Models/DTOs/BuildingDTO.cs
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
