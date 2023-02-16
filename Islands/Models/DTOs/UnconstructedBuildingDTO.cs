using Islands.Models.Enums;

namespace Islands.DTOs
{
    public class UnconstructedBuildingDTO
    {
        public BuildingType Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CoinsForBuild { get; set; }
        public int IronsForBuild { get; set; }
        public int StonesForBuild { get; set; }
        public int WoodsForBuild { get; set; }
    }
}
