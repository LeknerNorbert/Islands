using Islands.DTOs;
using Islands.Models.Enums;
using Newtonsoft.Json;

namespace Islands.Services.IslandService
{
    public class GameConfigurationService : IGameConfigurationService
    {
        private readonly string[] unconstructedBuildingPaths = new string[]
        {
            @"..\Islands\GameConfigurationFiles\UnconstructedBuildings\Church.json",
            @"..\Islands\GameConfigurationFiles\UnconstructedBuildings\Factory.json",
            @"..\Islands\GameConfigurationFiles\UnconstructedBuildings\LumberYard.json",
            @"..\Islands\GameConfigurationFiles\UnconstructedBuildings\Mine.json",
            @"..\Islands\GameConfigurationFiles\UnconstructedBuildings\PracticeRange.json",
        };

        public async Task<BuildingWithDefaultValuesDto> GetBuildingDefaultValueAsync(BuildingType buildingType, int level)
        {
            string path = Path.Combine(@"..\Islands\GameConfigurationFiles\Buildings", $"{buildingType}-{level}.json");
            
            if(!File.Exists(path))
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            string configuration = await File.ReadAllTextAsync(path);
            BuildingWithDefaultValuesDto? building = JsonConvert.DeserializeObject<BuildingWithDefaultValuesDto>(configuration);

            if (building == null)
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            return building;
        }

        public async Task<List<UnconstructedBuildingDto>> GetUnconstructedBuildingsAsync()
        {
            List<UnconstructedBuildingDto> unconstructedBuildings = new();

            foreach(string path in unconstructedBuildingPaths)
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Configuration file not found.");
                }

                string configuration = await File.ReadAllTextAsync(path);
                UnconstructedBuildingDto? unconstructedBuilding = JsonConvert.DeserializeObject<UnconstructedBuildingDto>(configuration);

                if (unconstructedBuilding == null)
                {
                    throw new FileNotFoundException("Configuration file not found.");
                }

                unconstructedBuildings.Add(unconstructedBuilding);
            }

            return unconstructedBuildings;
        }

        public async Task<SkillsDto> GetDefaultSkillsByIslandAsync(IslandType islandType)
        {
            string path = Path.Combine(@"..\Islands\GameConfigurationFiles\DefaultSkills", $"{islandType}.json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            string configuration = await File.ReadAllTextAsync(path);
            SkillsDto? skills = JsonConvert.DeserializeObject<SkillsDto>(configuration);

            if(skills == null)
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            return skills;
        }

        public async Task<SkillsDto> GetMaximumSkillPointsAsync()
        {
            string path = @"..\Islands\GameConfigurationFiles\MaxSkillPoints\MaxSkillPoints.json";

            if(!File.Exists(path))
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            string configuration = await File.ReadAllTextAsync(path);
            SkillsDto? skills = JsonConvert.DeserializeObject<SkillsDto>(configuration);

            if (skills == null)
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            return skills;
        }

        public async Task<IslandDto> GetIslandAsync(IslandType islandType)
        {
            string path = Path.Combine(@"..\Islands\GameConfigurationFiles\Islands", $"{islandType}.json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            string configuration = await File.ReadAllTextAsync(path);
            IslandDto? island = JsonConvert.DeserializeObject<IslandDto>(configuration);

            if (island == null)
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            return island;
        }

        public int GetSkillPointsByLevel(int experiences)
        {
            return Convert.ToInt32(Math.Floor(Math.Sqrt(experiences)));
        }
    }
}
