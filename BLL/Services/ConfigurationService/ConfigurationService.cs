using DAL.DTOs;
using DAL.Models.Enums;
using Newtonsoft.Json;

namespace BLL.Services.ConfigurationService
{
    public class ConfigurationService : IConfigurationService
    {
        public async Task<List<UnbuiltBuildingDto>> GetAllUnbuiltBuildingsByIslandAsync(IslandType islandType)
        {
            string[] unbuiltPaths = new string[]
            {
                @$"..\BLL\ConfigurationFiles\UnbuiltBuildings\{islandType}\Church.json",
                @$"..\BLL\ConfigurationFiles\UnbuiltBuildings\{islandType}\Factory.json",
                @$"..\BLL\ConfigurationFiles\UnbuiltBuildings\{islandType}\LumberYard.json",
                @$"..\BLL\ConfigurationFiles\UnbuiltBuildings\{islandType}\Mine.json",
                @$"..\BLL\ConfigurationFiles\UnbuiltBuildings\{islandType}\PracticeRange.json",
            };

            List<UnbuiltBuildingDto> unbuiltBuildings = new();

            foreach (string path in unbuiltPaths)
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("File not found.");
                }

                string json = await File.ReadAllTextAsync(path);
                UnbuiltBuildingDto? unbuiltBuilding = JsonConvert.DeserializeObject<UnbuiltBuildingDto>(json);

                if (unbuiltBuilding == null)
                {
                    throw new FileNotFoundException("File not found.");
                }

                unbuiltBuildings.Add(unbuiltBuilding);
            }

            return unbuiltBuildings;
        }

        public async Task<BuildingConfigurationDto> GetBuildingByIslandAsync(IslandType islandType, BuildingType buildingType, int level)
        {
            string path = @$"..\BLL\ConfigurationFiles\Buildings\{islandType}\{buildingType}-{level}.json";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.");
            }

            string json = await File.ReadAllTextAsync(path);
            BuildingConfigurationDto? building = JsonConvert.DeserializeObject<BuildingConfigurationDto>(json);

            if (building == null)
            {
                throw new FileNotFoundException("File not found.");
            }

            return building;
        }

        public async Task<SkillsDto> GetDefaultSkillsByIslandAsync(IslandType islandType)
        {
            string path = @$"..\BLL\ConfigurationFiles\DefaultSkills\{islandType}.json";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.");
            }

            string json = await File.ReadAllTextAsync(path);
            SkillsDto? skills = JsonConvert.DeserializeObject<SkillsDto>(json);

            if (skills == null)
            {
                throw new FileNotFoundException("File not found.");
            }

            return skills;
        }

        public async Task<EnemyConfigurationDto> GetEnemyByIslandAsync(IslandType islandType)
        {
            string path = @$"..\BLL\ConfigurationFiles\Enemies\{islandType}.json";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.");
            }

            string json = await File.ReadAllTextAsync(path);
            EnemyConfigurationDto? enemy = JsonConvert.DeserializeObject<EnemyConfigurationDto>(json);

            if (enemy == null)
            {
                throw new FileNotFoundException("File not found.");
            }

            return enemy;
        }

        public int GetExperienceByLevel(int level)
        {
            return Convert.ToInt32(Math.Pow((level / 0.1), 2));
        }

        public async Task<IslandDto> GetIslandAsync(IslandType islandType)
        {
            string path = @$"..\BLL\ConfigurationFiles\Islands\{islandType}.json";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.");
            }

            string json = await File.ReadAllTextAsync(path);
            IslandDto? island = JsonConvert.DeserializeObject<IslandDto>(json);

            if (island == null)
            {
                throw new FileNotFoundException("File not found.");
            }

            return island;
        }

        public int GetLevelByExperience(int experiences)
        {
            return Convert.ToInt32(0.1 * Math.Sqrt(experiences));
        }

        public async Task<SkillsDto> GetMaximumSkillPointsAsync()
        {
            string path = @"..\BLL\ConfigurationFiles\MaxSkillPoints\MaxSkillPoints.json";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.");
            }

            string json = await File.ReadAllTextAsync(path);
            SkillsDto? skills = JsonConvert.DeserializeObject<SkillsDto>(json);

            if (skills == null)
            {
                throw new FileNotFoundException("File not found.");
            }

            return skills;
        }

        public async Task<ProfileImageConfigurationDto> GetProfileImageByIslandAsync(IslandType islandType)
        {
            string path = @$"..\BLL\ConfigurationFiles\ProfileImages\{islandType}.json";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.");
            }

            string json = await File.ReadAllTextAsync(path);
            ProfileImageConfigurationDto? profileImage = JsonConvert.DeserializeObject<ProfileImageConfigurationDto>(json);

            if (profileImage == null)
            {
                throw new FileNotFoundException("File not found.");
            }

            return profileImage;
        }
    }
}