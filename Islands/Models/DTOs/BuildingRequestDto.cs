using Islands.Models.Enums;

namespace Islands.Models.DTOs
{
    public class BuildingRequestDto
    {
        public BuildingType Type { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
    }
}
