using Islands.Models.Enums;

namespace Islands.DTOs
{
    public class UnconstructedBuildingDto
    {
        public BuildingType Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CoinsForBuild { get; set; }
        public int IronsForBuild { get; set; }
        public int StonesForBuild { get; set; }
        public int WoodsForBuild { get; set; }
    }
}
