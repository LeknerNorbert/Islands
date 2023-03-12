﻿using DAL.Models.Enums;

namespace DAL.DTOs
{
    public class BuildRequestDto
    {
        public BuildingType Type { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
    }
}