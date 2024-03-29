﻿using BLL.Exceptions;
using BLL.Services.ConfigurationService;
using DAL.DTOs;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.BuildingRepository;
using DAL.Repositories.PlayerRepository;
using System.Diagnostics;

namespace BLL.Services.BuildingService
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IConfigurationService _configurationService;

        public BuildingService(
            IConfigurationService gameConfigurationService,
            IBuildingRepository buildingRepository,
            IPlayerRepository playerRepository)
        {
            this._configurationService = gameConfigurationService;
            this._buildingRepository = buildingRepository;
            this._playerRepository = playerRepository;
        }

        public async Task<BuildingDto> AddBuildingAsync(string username, BuildRequestDto buildingRequest)
        {
            if (await _buildingRepository.CheckBuildingIsExist(username, buildingRequest.BuildingType))
            {
                throw new BuildingAlreadyExistException("Building already exist.");
            }

            List<CoordinateDto> reservedCoordinates = await _buildingRepository
                .GetReservedCoordinates(username);

            if (reservedCoordinates.Exists(coordinate => 
                coordinate.XCoordinate == buildingRequest.XCoordinate &&
                coordinate.YCoordinate == buildingRequest.YCoordinate))
            {
                throw new InvalidCoordinatesException("Invalid coordinates.");
            }

            Player player = await _playerRepository
                .GetPlayerByUsernameAsync(username);
            BuildingConfigurationDto buildingConfiguration = await _configurationService
                .GetBuildingByIslandAsync(player.SelectedIsland, buildingRequest.BuildingType, 1);

            if (player.Coins < buildingConfiguration.CoinsForBuild ||
                player.Woods < buildingConfiguration.WoodsForBuild ||
                player.Stones < buildingConfiguration.StonesForBuild ||
                player.Irons < buildingConfiguration.IronsForBuild)
            {
                throw new NotEnoughItemsException("There are no enough items for build.");
            }

            player.Experience += buildingConfiguration.ExperienceReward;
            player.Coins -= buildingConfiguration.CoinsForBuild;
            player.Woods -= buildingConfiguration.WoodsForBuild;
            player.Stones -= buildingConfiguration.StonesForBuild;
            player.Irons -= buildingConfiguration.IronsForBuild;

            Building building = new()
            {
                BuildingType = buildingRequest.BuildingType,
                XCoordinate = buildingRequest.XCoordinate,
                YCoordinate = buildingRequest.YCoordinate,
                Level = 1,
                BuildDate = DateTime.Now.AddMilliseconds(buildingConfiguration.BuildTime),
                LastCollectDate = DateTime.Now.AddMilliseconds(buildingConfiguration.BuildTime),
                Player = player,
            };

            await _playerRepository.UpdatePlayerAsync(player);
            int id = await _buildingRepository.AddBuildingAsync(building);

            return new BuildingDto()
            {
                Id = id,
                BuildingType = buildingConfiguration.BuildingType,
                Name = buildingConfiguration.Name,
                XCoordinate = building.XCoordinate,
                YCoordinate = building.YCoordinate,
                Level = building.Level,
                MaxLevel = buildingConfiguration.MaxLevel,
                Description = buildingConfiguration.Description,
                SpritePath = buildingConfiguration.SpritePath,
                CoinsForBuild = buildingConfiguration.CoinsForBuild,
                IronsForBuild = buildingConfiguration.IronsForBuild,
                StonesForBuild = buildingConfiguration.StonesForBuild,
                WoodsForBuild = buildingConfiguration.WoodsForBuild,
                ProductionInterval = buildingConfiguration.ProductionInterval,
                ProducedCoins = buildingConfiguration.ProducedCoins,
                ProducedIrons = buildingConfiguration.ProducedIrons,
                ProducedStones = buildingConfiguration.ProducedStones,
                ProducedWoods = buildingConfiguration.ProducedWoods,
                ExperienceReward = buildingConfiguration.ExperienceReward,
                BuildDate = building.BuildDate,
                LastCollectDate = building.LastCollectDate
            };
        }
        public async Task<List<BuildingDto>> GetAllBuildingsAsync(string username)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            List<Building> buildings = await _buildingRepository.GetAllBuildingsByUsernameAsync(username);

            return buildings
                .Select(async building =>
                {
                    BuildingConfigurationDto buildingConfiguration =
                        await _configurationService.GetBuildingByIslandAsync(player.SelectedIsland, building.BuildingType, building.Level);

                    return new BuildingDto()
                    {
                        Id = building.Id,
                        BuildingType = building.BuildingType.ToString(),
                        Name = buildingConfiguration.Name,
                        XCoordinate = building.XCoordinate,
                        YCoordinate = building.YCoordinate,
                        Level = building.Level,
                        MaxLevel = buildingConfiguration.MaxLevel,
                        Description = buildingConfiguration.Description,
                        SpritePath = buildingConfiguration.SpritePath,
                        CoinsForBuild = buildingConfiguration.CoinsForBuild,
                        IronsForBuild = buildingConfiguration.IronsForBuild,
                        StonesForBuild = buildingConfiguration.StonesForBuild,
                        WoodsForBuild = buildingConfiguration.WoodsForBuild,
                        ProductionInterval = buildingConfiguration.ProductionInterval,
                        ProducedCoins = buildingConfiguration.ProducedCoins,
                        ProducedIrons = buildingConfiguration.ProducedIrons,
                        ProducedStones = buildingConfiguration.ProducedStones,
                        ProducedWoods = buildingConfiguration.ProducedWoods,
                        ExperienceReward = buildingConfiguration.ExperienceReward,
                        MaximumProductionCount = buildingConfiguration.MaximumProductionCount,
                        BuildDate = building.BuildDate,
                        LastCollectDate = building.LastCollectDate
                    };
                })
                .Select(task => task.Result)
                .ToList();
        }

        public async Task<List<UnbuiltBuildingDto>> GetAllUnbuiltBuildingsAsync(string username)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            List<BuildingConfigurationDto> buildings = await _configurationService.GetAllUnbuiltBuildingsByIslandAsync(player.SelectedIsland);

            return buildings.Select(building => new UnbuiltBuildingDto()
            {
                BuildingType = building.BuildingType,
                Name = building.Name,
                Description = building.Description,
                SpritePath = building.SpritePath,
                CoinsForBuild = building.CoinsForBuild,
                IronsForBuild = building.IronsForBuild,
                StonesForBuild = building.StonesForBuild,
                WoodsForBuild = building.WoodsForBuild,
                ExperienceReward = building.ExperienceReward
            }).ToList();
        }

        public async Task<UnbuiltBuildingDto> GetNextLevelOfBuildingByIslandAsync(string username, BuildingType buildingType)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            Building building = await _buildingRepository.GetBuildingAsync(username, buildingType);
            BuildingConfigurationDto nextLevelBuilding = await _configurationService
                .GetBuildingByIslandAsync(player.SelectedIsland, buildingType, building.Level + 1);

            return new UnbuiltBuildingDto()
            {
                BuildingType = nextLevelBuilding.BuildingType,
                Name = nextLevelBuilding.Name,
                Description = nextLevelBuilding.Description,
                SpritePath = nextLevelBuilding.SpritePath,
                CoinsForBuild = nextLevelBuilding.CoinsForBuild,
                IronsForBuild = nextLevelBuilding.IronsForBuild,
                StonesForBuild = nextLevelBuilding.StonesForBuild,
                WoodsForBuild = nextLevelBuilding.WoodsForBuild,
                ExperienceReward = nextLevelBuilding.ExperienceReward
            };
        }

        public async Task<BuildingDto> UpgradeBuildingAsync(string username, BuildingType type)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            Building building = await _buildingRepository.GetBuildingAsync(username, type);
            BuildingConfigurationDto currentConfiguration = await _configurationService
                .GetBuildingByIslandAsync(player.SelectedIsland, building.BuildingType, building.Level);

            if (currentConfiguration.MaxLevel <= building.Level)
            {
                throw new MaxLevelReachedException("Failed to upgrade, max level reached.");
            }

            BuildingConfigurationDto nextLevelConfiguration =
                await _configurationService.GetBuildingByIslandAsync(player.SelectedIsland, building.BuildingType, building.Level + 1);


            if (player.Coins < nextLevelConfiguration.CoinsForBuild ||
                player.Woods < nextLevelConfiguration.WoodsForBuild ||
                player.Stones < nextLevelConfiguration.StonesForBuild ||
                player.Irons < nextLevelConfiguration.IronsForBuild)
            {
                throw new NotEnoughItemsException("There are no enough items for upgrade.");
            }

            building.Level += 1;
            building.LastCollectDate = DateTime.Now.AddMilliseconds(nextLevelConfiguration.BuildTime);
            building.BuildDate = DateTime.Now.AddMilliseconds(nextLevelConfiguration.BuildTime);

            player.Coins -= nextLevelConfiguration.CoinsForBuild;
            player.Woods -= nextLevelConfiguration.WoodsForBuild;
            player.Stones -= nextLevelConfiguration.StonesForBuild;
            player.Irons -= nextLevelConfiguration.IronsForBuild;
            player.Experience += nextLevelConfiguration.ExperienceReward;

            await _buildingRepository.UpdateBuildingAsync(building);
            await _playerRepository.UpdatePlayerAsync(player);

            return new BuildingDto()
            {
                Id = building.Id,
                BuildingType = nextLevelConfiguration.BuildingType,
                Name = nextLevelConfiguration.Name,
                XCoordinate = building.XCoordinate,
                YCoordinate = building.YCoordinate,
                Level = building.Level,
                MaxLevel = nextLevelConfiguration.MaxLevel,
                Description = nextLevelConfiguration.Description,
                SpritePath = nextLevelConfiguration.SpritePath,
                CoinsForBuild = nextLevelConfiguration.CoinsForBuild,
                IronsForBuild = nextLevelConfiguration.IronsForBuild,
                StonesForBuild = nextLevelConfiguration.StonesForBuild,
                WoodsForBuild = nextLevelConfiguration.WoodsForBuild,
                ProductionInterval = nextLevelConfiguration.ProductionInterval,
                ProducedCoins = nextLevelConfiguration.ProducedCoins,
                ProducedIrons = nextLevelConfiguration.ProducedIrons,
                ProducedStones = nextLevelConfiguration.ProducedStones,
                ProducedWoods = nextLevelConfiguration.ProducedWoods,
                ExperienceReward = nextLevelConfiguration.ExperienceReward,
                MaximumProductionCount = nextLevelConfiguration.MaximumProductionCount,
                BuildDate = DateTime.Now.AddMilliseconds(nextLevelConfiguration.BuildTime),
                LastCollectDate = DateTime.Now.AddMilliseconds(nextLevelConfiguration.BuildTime)
            };
        }
        public async Task<ItemsDto> CollectItemsAsync(string username, CollectRequestDto collectRequest)
        {
            Player player = await _playerRepository
                .GetPlayerByUsernameAsync(username);

            Building building = await _buildingRepository
                .GetBuildingAsync(username, collectRequest.BuildingType);

            BuildingConfigurationDto buildingConfiguration = await _configurationService
                .GetBuildingByIslandAsync(player.SelectedIsland, building.BuildingType, building.Level);

            int productionsFromLastCollection = 0;
            DateTime currentProductionDate = building.BuildDate;

            while(currentProductionDate < collectRequest.CollectDate)
            {
                if (currentProductionDate > building.LastCollectDate)
                {
                    productionsFromLastCollection++;
                }

                currentProductionDate = currentProductionDate.AddMilliseconds(buildingConfiguration.ProductionInterval);
            }

            int collectedCoins = productionsFromLastCollection * buildingConfiguration.ProducedCoins;
            int collectedWoods = productionsFromLastCollection * buildingConfiguration.ProducedWoods;
            int collectedStones = productionsFromLastCollection * buildingConfiguration.ProducedStones;
            int collectedIrons = productionsFromLastCollection * buildingConfiguration.ProducedIrons;

            if (collectedCoins > buildingConfiguration.MaximumProductionCount)
            {
                collectedCoins = buildingConfiguration.MaximumProductionCount;
            }

            if (collectedWoods > buildingConfiguration.MaximumProductionCount)
            {
                collectedWoods = buildingConfiguration.MaximumProductionCount;
            }

            if (collectedStones > buildingConfiguration.MaximumProductionCount)
            {
                collectedStones = buildingConfiguration.MaximumProductionCount;
            }

            if (collectedIrons > buildingConfiguration.MaximumProductionCount)
            {
                collectedIrons = buildingConfiguration.MaximumProductionCount;
            }

            player.Coins += collectedCoins;
            player.Woods += collectedWoods;
            player.Stones += collectedStones;
            player.Irons += collectedIrons;
            building.LastCollectDate = collectRequest.CollectDate;

            await _playerRepository.UpdatePlayerAsync(player);
            await _buildingRepository.UpdateBuildingAsync(building);

            return new ItemsDto(collectedCoins, collectedWoods, collectedStones, collectedIrons);
        }
    }
}