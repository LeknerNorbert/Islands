using Islands.DTOs;
using Islands.Models.Enums;

namespace Islands.Services.IslandService
{
    public interface IGameConfigurationService
    {
        public BuildingWithDefaultValuesDto GetBuildingDefaultValue(BuildingType building, int level);
        public List<UnconstructedBuildingDto> GetUnconstructedBuildings();
        public SkillsDto GetDefaultSkillsByIsland(IslandType island);
        public SkillsDto GetMaximumSkillPoints();
        public IslandDto GetIsland(IslandType island);
        public int GetSkillPointsByLevel(int experiences);
    }
}
