using System.Numerics;

namespace Islands.DTOs
{
    public class IslandDTO
    {
        public string? Background { get; set; }
        public Vector2[]? ConstructionAreas { get; set; }
        public Vector2[]? NPCRoutes { get; set; }
        public string[]? NPCSprites { get; set; }
    }
}
