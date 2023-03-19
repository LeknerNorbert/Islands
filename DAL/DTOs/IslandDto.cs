using System.Numerics;

namespace DAL.DTOs
{
    public class IslandDto
    {
        public string SpritePath { get; set; } = string.Empty;
        public CoordinateDto[] BuildingAreas { get; set; } = Array.Empty<CoordinateDto>();
        public CoordinateDto[] NPCRoutes { get; set; } = Array.Empty<CoordinateDto>();
        public string[] NPCSprites { get; set; } = Array.Empty<string>();
    }
}
