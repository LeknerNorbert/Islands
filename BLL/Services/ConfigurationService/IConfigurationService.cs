using DAL.DTOs;
using DAL.Models.Enums;

namespace BLL.Services.ConfigurationService
{
    public interface IConfigurationService
    {
        Task<BuildingConfigurationDto> GetBuildingAsync(BuildingType name, int level);
        Task<List<UnbuiltBuildingDto>> GetAllUnbuiltBuildingAsync();
        Task<SkillsDto> GetDefaultSkillsByIslandAsync(IslandType name);
        Task<SkillsDto> GetMaximumSkillPointsAsync();
        Task<IslandDto> GetIslandAsync(IslandType name);
        Task<EnemyConfigurationDto> GetEnemy(IslandType name);
        int GetExperienceByLevel(int level);
        int GetLevelByExperience(int experiences);
    }
}
