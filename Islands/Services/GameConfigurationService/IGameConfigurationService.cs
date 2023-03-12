using Islands.DTOs;
using Islands.Models.Enums;

namespace Islands.Services.IslandService
{
    public interface IGameConfigurationService
    {
        Task<BuildingWithDefaultValuesDto> GetBuildingDefaultValueAsync(BuildingType building, int level);
        Task<List<UnconstructedBuildingDto>> GetUnconstructedBuildingsAsync();
        Task<SkillsDto> GetDefaultSkillsByIslandAsync(IslandType island);
        Task<SkillsDto> GetMaximumSkillPointsAsync();
        Task<IslandDto> GetIslandAsync(IslandType island);
        int GetExperienceByLevel(int level);    
        int GetLevelByExperience(int experiences);
    }
}
