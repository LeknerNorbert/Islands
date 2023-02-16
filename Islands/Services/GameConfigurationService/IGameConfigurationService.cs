using Islands.DTOs;
using Islands.Models.Enums;

namespace Islands.Services.IslandService
{
    public interface IGameConfigurationService
    {
        public BuildingWithDefaultValuesDTO GetBuildingDefaultValue(BuildingType building, int level);
        public List<UnconstructedBuildingDTO> GetUnconstructedBuildings();
        public SkillsDTO GetDefaultSkillsByIsland(IslandType island);
        public SkillsDTO GetMaximumSkillPoints();
        public IslandDTO GetIsland(IslandType island);
    }
}
