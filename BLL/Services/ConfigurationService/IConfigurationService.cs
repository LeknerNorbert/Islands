using DAL.DTOs;
using DAL.Models.Enums;

namespace BLL.Services.ConfigurationService
{
    public interface IConfigurationService
    {
        Task<BuildingConfigurationDto> GetBuildingByIslandAsync(IslandType islandType, BuildingType buildingType, int level);
        Task<List<UnbuiltBuildingDto>> GetAllUnbuiltBuildingsByIslandAsync(IslandType islandType);
        Task<SkillsDto> GetDefaultSkillsByIslandAsync(IslandType islandType);
        Task<SkillsDto> GetMaximumSkillPointsAsync();
        Task<IslandDto> GetIslandAsync(IslandType islandType);
        Task<EnemyConfigurationDto> GetEnemyByIslandAsync(IslandType islandType);
        Task<ProfileImageConfigurationDto> GetProfileImageByIslandAsync(IslandType islandType);
        int GetExperienceByLevel(int level);
        int GetLevelByExperience(int experiences);
    }
}
