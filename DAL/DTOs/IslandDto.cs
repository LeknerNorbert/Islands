using System.Numerics;

namespace DAL.DTOs
{
    public class IslandDto
    {
        public string SpritePath { get; set; } = string.Empty;
        public List<CoordinateDto> ConstructionAreas { get; set; } = new List<CoordinateDto>();
        public List<CoordinateDto> NPCRoutes { get; set; }
        public string[] NPCSprites { get; set; } = Array.Empty<string>();

        public IslandDto()
        {
            ConstructionAreas = new();
            NPCRoutes = new();
        }
    }
}
