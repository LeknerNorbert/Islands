using Islands.DTOs;
using Islands.Models.Enums;
using Newtonsoft.Json;

namespace Islands.Services.IslandService
{
    public class GameConfigurationService : IGameConfigurationService
    {
        private Dictionary<string, BuildingDefaultValuesDTO> _buildings;
        private List<UnconstructedBuildingDTO> _unconstructedBuildings;
        private Dictionary<string, SkillsDTO> _defaultSkills;
        private Dictionary<string, IslandDTO> _islands;
        private SkillsDTO _maxSkillPoints;

        private string[] _buildingPaths = new string[]
        {
            @"..\BLL\GameConfigurationFiles\Buildings\Church-1.json",
            @"..\BLL\GameConfigurationFiles\Buildings\Church-2.json",
            @"..\BLL\GameConfigurationFiles\Buildings\Church-3.json",
            @"..\BLL\GameConfigurationFiles\Buildings\Factory-1.json",
            @"..\BLL\GameConfigurationFiles\Buildings\Factory-2.json",
            @"..\BLL\GameConfigurationFiles\Buildings\Factory-3.json",
            @"..\BLL\GameConfigurationFiles\Buildings\LumberYard-1.json",
            @"..\BLL\GameConfigurationFiles\Buildings\LumberYard-2.json",
            @"..\BLL\GameConfigurationFiles\Buildings\LumberYard-3.json",
            @"..\BLL\GameConfigurationFiles\Buildings\Mine-1.json",
            @"..\BLL\GameConfigurationFiles\Buildings\Mine-2.json",
            @"..\BLL\GameConfigurationFiles\Buildings\Mine-3.json",
            @"..\BLL\GameConfigurationFiles\Buildings\PracticeRange-1.json",
            @"..\BLL\GameConfigurationFiles\Buildings\PracticeRange-2.json",
            @"..\BLL\GameConfigurationFiles\Buildings\PracticeRange-3.json",
        };

        private string[] _unconstructedBuildingPaths = new string[]
        {
            @"..\BLL\GameConfigurationFiles\UnconstructedBuildings\Church.json",
            @"..\BLL\GameConfigurationFiles\UnconstructedBuildings\Factory.json",
            @"..\BLL\GameConfigurationFiles\UnconstructedBuildings\LumberYard.json",
            @"..\BLL\GameConfigurationFiles\UnconstructedBuildings\Mine.json",
            @"..\BLL\GameConfigurationFiles\UnconstructedBuildings\PracticeRange.json",
        };

        private string[] _defaultSkillsPaths = new string[]
        {
            @"..\BLL\GameConfigurationFiles\DefaultSkills\Europian.json",
            @"..\BLL\GameConfigurationFiles\DefaultSkills\Indian.json",
            @"..\BLL\GameConfigurationFiles\DefaultSkills\Japan.json",
            @"..\BLL\GameConfigurationFiles\DefaultSkills\Viking.json",
        };

        private string[] _islandPaths = new string[]
        {
            @"..\BLL\GameConfigurationFiles\Islands\Europian.json",
            @"..\BLL\GameConfigurationFiles\Islands\Indian.json",
            @"..\BLL\GameConfigurationFiles\Islands\Japan.json",
            @"..\BLL\GameConfigurationFiles\Islands\Viking.json",
        };

        private string _maxSkillPointsPath = @"..\BLL\GameConfigurationFiles\MaxSkillPoints\MaxSkillPoints.json";

        public GameConfigurationService()
        {
            _buildings = new Dictionary<string, BuildingDefaultValuesDTO>();
            _unconstructedBuildings = new();
            _defaultSkills = new Dictionary<string, SkillsDTO>();
            _islands = new Dictionary<string, IslandDTO>();

            SetConfigFiles();
        }

        public BuildingDefaultValuesDTO GetBuildingDefaultValue(BuildingType building, int level)
        {
            return _buildings[$"{building}-{level}"];
        }

        public List<UnconstructedBuildingDTO> GetUnconstructedBuildings()
        {
            return _unconstructedBuildings;
        }

        public SkillsDTO GetDefaultSkillsByIsland(IslandType island)
        {
            return _defaultSkills[island.ToString()];
        }

        public SkillsDTO GetMaximumSkillPoints()
        {
            return _maxSkillPoints;
        }

        public IslandDTO GetIsland(IslandType island)
        {
            return _islands[island.ToString()];
        }

        private void SetConfigFiles()
        {
            foreach (string buildingPath in _buildingPaths)
            {
                if (File.Exists(buildingPath))
                {
                    string configurationFile = File.ReadAllText(buildingPath);
                    string key = Path.GetFileNameWithoutExtension(buildingPath);

                    BuildingDefaultValuesDTO? building = JsonConvert.DeserializeObject<BuildingDefaultValuesDTO>(configurationFile);
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

                    UnconstructedBuildingDTO? unconstructedBuilding = JsonConvert.DeserializeObject<UnconstructedBuildingDTO>(configurationFile);
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

                    SkillsDTO? defaultSkills = JsonConvert.DeserializeObject<SkillsDTO>(configurationFile);
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
                _maxSkillPoints = JsonConvert.DeserializeObject<SkillsDTO>(configurationFile);
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

                    IslandDTO? island = JsonConvert.DeserializeObject<IslandDTO>(configurationFile);
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
