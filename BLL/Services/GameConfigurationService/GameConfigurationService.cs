using BLL.DTOs;
using DAL.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IslandService
{
    public class GameConfigurationService : IGameConfigurationService
    {
        private Dictionary<string, BuildingDefaultValuesDto> _buildings;
        private List<UnconstructedBuildingDto> _unconstructedBuildings;
        private Dictionary<string, SkillsDto> _defaultSkills;
        private Dictionary<string, IslandDto> _islands;
        private SkillsDto _maxSkillPoints;

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
            _buildings = new Dictionary<string, BuildingDefaultValuesDto>();
            _unconstructedBuildings = new();
            _defaultSkills = new Dictionary<string, SkillsDto>();
            _islands = new Dictionary<string, IslandDto>();

            SetConfigFiles();
        }

        public BuildingDefaultValuesDto GetBuildingDefaultValue(BuildingType building, int level)
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

        private void SetConfigFiles()
        {
            foreach (string buildingPath in _buildingPaths)
            {
                if (File.Exists(buildingPath))
                {
                    string configurationFile = File.ReadAllText(buildingPath);
                    string key = Path.GetFileNameWithoutExtension(buildingPath);

                    BuildingDefaultValuesDto? building = JsonConvert.DeserializeObject<BuildingDefaultValuesDto>(configurationFile);
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
