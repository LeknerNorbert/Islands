using DAL.Models.Enums;

namespace DAL.DTOs
{
    public class UnbuiltBuildingDto
    {
        public string BuildingType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SpritePath { get; set; } = string.Empty;
        public int CoinsForBuild { get; set; }
        public int IronsForBuild { get; set; }
        public int StonesForBuild { get; set; }
        public int WoodsForBuild { get; set; }
    }
}
