using System.Numerics;

namespace Islands.DTOs
{
    public class IslandDTO
    {
        public string Background { get; set; } = string.Empty;
        public Vector2[]? ConstructionAreas { get; set; } = Array.Empty<Vector2>();
        public Vector2[]? NPCRoutes { get; set; } = Array.Empty<Vector2>();
        public string[] NPCSprites { get; set; } = Array.Empty<string>();
    }
}
