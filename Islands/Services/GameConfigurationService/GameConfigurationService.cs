using Islands.DTOs;
using Islands.Models.Enums;
using Newtonsoft.Json;

namespace Islands.Services.IslandService
{
    public class GameConfigurationService : IGameConfigurationService
    {
        private readonly Dictionary<string, BuildingWithDefaultValuesDto> _buildings;
        private readonly List<UnconstructedBuildingDto> _unconstructedBuildings;
        private readonly Dictionary<string, SkillsDto> _defaultSkills;
        private readonly Dictionary<string, IslandDto> _islands;
        private SkillsDto _maxSkillPoints;

        private readonly string[] _buildingPaths = new string[]
        {
            @"..\Islands\GameConfigurationFiles\Buildings\Church-1.json",
            @"..\Islands\GameConfigurationFiles\Buildings\Church-2.json",
            @"..\Islands\GameConfigurationFiles\Buildings\Church-3.json",
            @"..\Islands\GameConfigurationFiles\Buildings\Factory-1.json",
            @"..\Islands\GameConfigurationFiles\Buildings\Factory-2.json",
            @"..\Islands\GameConfigurationFiles\Buildings\Factory-3.json",
            @"..\Islands\GameConfigurationFiles\Buildings\LumberYard-1.json",
            @"..\Islands\GameConfigurationFiles\Buildings\LumberYard-2.json",
            @"..\Islands\GameConfigurationFiles\Buildings\LumberYard-3.json",
            @"..\Islands\GameConfigurationFiles\Buildings\Mine-1.json",
            @"..\Islands\GameConfigurationFiles\Buildings\Mine-2.json",
            @"..\Islands\GameConfigurationFiles\Buildings\Mine-3.json",
            @"..\Islands\GameConfigurationFiles\Buildings\PracticeRange-1.json",
            @"..\Islands\GameConfigurationFiles\Buildings\PracticeRange-2.json",
            @"..\Islands\GameConfigurationFiles\Buildings\PracticeRange-3.json",
        };

        private readonly string[] _unconstructedBuildingPaths = new string[]
        {
            @"..\Islands\GameConfigurationFiles\UnconstructedBuildings\Church.json",
            @"..\Islands\GameConfigurationFiles\UnconstructedBuildings\Factory.json",
            @"..\Islands\GameConfigurationFiles\UnconstructedBuildings\LumberYard.json",
            @"..\Islands\GameConfigurationFiles\UnconstructedBuildings\Mine.json",
            @"..\Islands\GameConfigurationFiles\UnconstructedBuildings\PracticeRange.json",
        };

        private readonly string[] _defaultSkillsPaths = new string[]
        {
            @"..\Islands\GameConfigurationFiles\DefaultSkills\Europian.json",
            @"..\Islands\GameConfigurationFiles\DefaultSkills\Indian.json",
            @"..\Islands\GameConfigurationFiles\DefaultSkills\Japan.json",
            @"..\Islands\GameConfigurationFiles\DefaultSkills\Viking.json",
        };

        private readonly string[] _islandPaths = new string[]
        {
            @"..\Islands\GameConfigurationFiles\Islands\Europian.json",
            @"..\Islands\GameConfigurationFiles\Islands\Indian.json",
            @"..\Islands\GameConfigurationFiles\Islands\Japan.json",
            @"..\Islands\GameConfigurationFiles\Islands\Viking.json",
        };

        private string _maxSkillPointsPath = @"..\Islands\GameConfigurationFiles\MaxSkillPoints\MaxSkillPoints.json";

        public GameConfigurationService()
        {
            _buildings = new Dictionary<string, BuildingWithDefaultValuesDto>();
            _unconstructedBuildings = new();
            _defaultSkills = new Dictionary<string, SkillsDto>();
            _islands = new Dictionary<string, IslandDto>();

            SetConfigFiles();
        }

        public BuildingWithDefaultValuesDto GetBuildingDefaultValue(BuildingType building, int level)
        {
            return _buildings[$"{building}-{level}"];
        }

        public List<UnconstructedBuildingDto> GetUnconstructedBuildings()
        {
            return _unconstructedBuildings;
        }

        public SkillsDto GetDefaultSkillsByIsland(IslandType island)
        {
            return _defaultSkills[island.ToString()];
        }

        public SkillsDto GetMaximumSkillPoints()
        {
            return _maxSkillPoints;
        }

        public IslandDto GetIsland(IslandType island)
        {
            return _islands[island.ToString()];
        }

        public int GetSkillPointsByLevel(int experiences)
        {
            return Convert.ToInt32(Math.Floor(Math.Sqrt(experiences)));
        }

        private void SetConfigFiles()
        {
            foreach (string buildingPath in _buildingPaths)
            {
                if (File.Exists(buildingPath))
                {
                    string configurationFile = File.ReadAllText(buildingPath);
                    string key = Path.GetFileNameWithoutExtension(buildingPath);

                    BuildingWithDefaultValuesDto? building = JsonConvert.DeserializeObject<BuildingWithDefaultValuesDto>(configurationFile);
                    _buildings.Add(key, building);
                }
                else
                {
                    throw new FileNotFoundException("Configuration file not found.");
                }
            }

            foreach (string unconstructedBuildingPath in _unconstructedBuildingPaths)
            {
                if (File.Exists(unconstructedBuildingPath))
                {
                    string configurationFile = File.ReadAllText(unconstructedBuildingPath);

                    UnconstructedBuildingDto? unconstructedBuilding = JsonConvert.DeserializeObject<UnconstructedBuildingDto>(configurationFile);
                    _unconstructedBuildings.Add(unconstructedBuilding);
                }
                else
                {
                    throw new FileNotFoundException("Configuration file not found.");
                }
            }

            foreach (string defaultSkillsPath in _defaultSkillsPaths)
            {
                if (File.Exists(defaultSkillsPath))
                {
                    string configurationFile = File.ReadAllText(defaultSkillsPath);
                    string key = Path.GetFileNameWithoutExtension(defaultSkillsPath);

                    SkillsDto? defaultSkills = JsonConvert.DeserializeObject<SkillsDto>(configurationFile);
                    _defaultSkills.Add(key, defaultSkills);
                }
                else
                {
                    throw new FileNotFoundException("Configuration file not found.");
                }
            }

            if(File.Exists(_maxSkillPointsPath))
            {
                string configurationFile = File.ReadAllText(_maxSkillPointsPath);
                _maxSkillPoints = JsonConvert.DeserializeObject<SkillsDto>(configurationFile);
            }
            else
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            foreach (string islandPath in _islandPaths)
            {
                if (File.Exists(islandPath))
                {
                    string configurationFile = File.ReadAllText(islandPath);
                    string key = Path.GetFileNameWithoutExtension(islandPath);

                    IslandDto? island = JsonConvert.DeserializeObject<IslandDto>(configurationFile);
                    _islands.Add(key, island);
                }
                else
                {
                    throw new FileNotFoundException("Configuration file not found.");
                }
            }
        }
    }
}
