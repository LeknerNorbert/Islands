using DAL.Models;
using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class BuildingDto
    {
        public int Id { get; set; }
        public BuildingType Name { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
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
        public DateTime BuildDate { get; set; }
        public DateTime LastCollectDate { get; set; }
    }
}
