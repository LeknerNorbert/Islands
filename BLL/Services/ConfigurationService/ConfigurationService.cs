using DAL.DTOs;
using DAL.Models.Enums;
using Newtonsoft.Json;

namespace BLL.Services.ConfigurationService
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly string[] unbuiltPaths = new string[]
{
            @"..\BLL\ConfigurationFiles\UnbuiltBuildings\Church.json",
            @"..\BLL\ConfigurationFiles\UnbuiltBuildings\Factory.json",
            @"..\BLL\ConfigurationFiles\UnbuiltBuildings\LumberYard.json",
            @"..\BLL\ConfigurationFiles\UnbuiltBuildings\Mine.json",
            @"..\BLL\ConfigurationFiles\UnbuiltBuildings\PracticeRange.json",
        };

        public async Task<List<UnbuiltBuildingDto>> GetAllUnbuiltBuildingAsync()
        {
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

        public async Task<BuildingConfigurationDto> GetBuildingAsync(BuildingType name, int level)
        {
            string path = Path.Combine(@"..\BLL\ConfigurationFiles\Buildings", $"{name}-{level}.json");

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

        public async Task<SkillsDto> GetDefaultSkillsByIslandAsync(IslandType name)
        {
            string path = Path.Combine(@"..\BLL\ConfigurationFiles\DefaultSkills", $"{name}.json");

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

        public async Task<EnemyConfigurationDto> GetEnemy(IslandType name)
        {
            string path = Path.Combine(@"..\BLL\ConfigurationFiles\Enemies", $"{name}.json");

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

        public async Task<IslandDto> GetIslandAsync(IslandType name)
        {
            string path = Path.Combine(@"..\BLL\ConfigurationFiles\Islands", $"{name}.json");

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
    }
}
