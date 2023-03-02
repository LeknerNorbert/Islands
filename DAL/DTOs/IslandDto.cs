using System.Numerics;

namespace DAL.DTOs
{
    public class IslandDto
    {
        public string SpritePath { get; set; } = string.Empty;
        public Vector2[] ConstructionAreas { get; set; } = Array.Empty<Vector2>();
        public Vector2[] NPCRoutes { get; set; } = Array.Empty<Vector2>();
        public string[] NPCSprites { get; set; } = Array.Empty<string>();
    }
}
