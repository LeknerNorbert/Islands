﻿using DAL.DTOs;
using DAL.Models.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BLL.Services.ConfigurationService
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly string rootPath;
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
            rootPath = _configuration.GetSection("RootPath").Value;
        }

        public async Task<List<BuildingConfigurationDto>> GetAllUnbuiltBuildingsByIslandAsync(IslandType islandType)
        {

            string[] unbuiltPaths = new string[]
            {
                Path.Combine(rootPath, "ConfigurationFiles", "Buildings", islandType.ToString(), "Church-1.json"),
                Path.Combine(rootPath, "ConfigurationFiles", "Buildings", islandType.ToString(), "Factory-1.json"),
                Path.Combine(rootPath, "ConfigurationFiles", "Buildings", islandType.ToString(), "LumberYard-1.json"),
                Path.Combine(rootPath, "ConfigurationFiles", "Buildings", islandType.ToString(), "Mine-1.json"),
                Path.Combine(rootPath, "ConfigurationFiles", "Buildings", islandType.ToString(), "PracticeRange-1.json"),
            };

            List<BuildingConfigurationDto> unbuiltBuildings = new();

            foreach (string path in unbuiltPaths)
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("File not found.");
                }

                string json = await File.ReadAllTextAsync(path);
                BuildingConfigurationDto? unbuiltBuilding = JsonConvert.DeserializeObject<BuildingConfigurationDto>(json);

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
            string path = Path.Combine(rootPath, "ConfigurationFiles", "Buildings", islandType.ToString(), $"{buildingType}-{level}.json"); 

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
            string path = Path.Combine(rootPath, "ConfigurationFiles", "DefaultSkills", $"{islandType}.json");


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
            string path = Path.Combine(rootPath, "ConfigurationFiles", "Enemies", $"{islandType}.json");

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
            string path = Path.Combine(rootPath, "ConfigurationFiles", "Islands", $"{islandType}.json");

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
            double sqrt = Math.Sqrt(experiences);
            return Convert.ToInt32(Math.Floor(sqrt * 0.1));
        }

        public async Task<SkillsDto> GetMaximumSkillPointsAsync()
        {
            string path = Path.Combine(rootPath, "ConfigurationFiles", "MaxSkillPoints", "MaxSkillPoints.json");

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
            string path = Path.Combine(rootPath, "ConfigurationFiles", "ProfileImages", $"{islandType}.json");

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