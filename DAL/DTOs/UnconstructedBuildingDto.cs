<<<<<<<< HEAD:DAL/DTOs/UnconstructedBuildingDto.cs
﻿using DAL.Models.Enums;

namespace DAL.DTOs
{
    public class UnconstructedBuildingDto
    {
        public string Name { get; set; } = string.Empty;
========
﻿using Islands.Models.Enums;

namespace Islands.DTOs
{
    public class UnconstructedBuildingDto
    {
        public BuildingType Name { get; set; }
>>>>>>>> master:Islands/Models/DTOs/UnconstructedBuildingDTO.cs
        public string Description { get; set; } = string.Empty;
        public int CoinsForBuild { get; set; }
        public int IronsForBuild { get; set; }
        public int StonesForBuild { get; set; }
        public int WoodsForBuild { get; set; }
        public int BuildTime { get; set; }
    }
}
