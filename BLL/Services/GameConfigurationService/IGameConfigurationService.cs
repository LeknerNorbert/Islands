using BLL.DTOs;
using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IslandService
{
    public interface IGameConfigurationService
    {
        public BuildingDefaultValuesDto GetBuildingDefaultValue(BuildingType building, int level);
        public List<UnconstructedBuildingDto> GetUnconstructedBuildings();
        public SkillsDto GetDefaultSkillsByIsland(IslandType island);
        public SkillsDto GetMaximumSkillPoints();
        public IslandDto GetIsland(IslandType island);
    }
}
