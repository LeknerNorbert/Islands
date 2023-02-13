using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class IslandDto
    {
        public string? Background { get; set; }
        public Vector2[]? ConstructionAreas { get; set; }
        public Vector2[]? NPCRoutes { get; set; }
        public string[]? NPCSprites { get; set; }
    }
}
