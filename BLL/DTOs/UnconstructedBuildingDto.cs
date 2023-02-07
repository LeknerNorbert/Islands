using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class UnconstructedBuildingDto
    {
        public BuildingType Name { get; set; }
        public string? Description { get; set; }
        public int CoinsForBuild { get; set; }
        public int IronsForBuild { get; set; }
        public int StonesForBuild { get; set; }
        public int WoodsForBuild { get; set; }
    }
}
