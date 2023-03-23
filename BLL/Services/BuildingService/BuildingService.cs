using BLL.Exceptions;
using BLL.Services.ConfigurationService;
using DAL.DTOs;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.BuildingRepository;
using DAL.Repositories.PlayerRepository;

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

            if (buildingRequest.XCoordinate > 25 ||
                buildingRequest.YCoordinate < 0 ||
                buildingRequest.YCoordinate > 15 ||
                buildingRequest.YCoordinate < 0)
            {
                throw new InvalidCoordinatesException("Invalid coordinates.");
            }

            List<CoordinateDto> reservedCoordinates = await _buildingRepository.GetReservedCoordinates(username);

            if (
                reservedCoordinates.FirstOrDefault(coordinate =>
                coordinate.YCoordinate == buildingRequest.YCoordinate &&
                coordinate.XCoordinate == buildingRequest.XCoordinate) != null)
            {
                throw new InvalidCoordinatesException("Invalid coordinates.");
            }

            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            BuildingConfigurationDto configuration =
                await _configurationService.GetBuildingByIslandAsync(player.SelectedIsland, buildingRequest.BuildingType, 1);

            Building building = new()
            {
                BuildingType = buildingRequest.BuildingType,
                XCoordinate = buildingRequest.XCoordinate,
                YCoordinate = buildingRequest.YCoordinate,
                Level = 1,
                BuildDate = DateTime.Now.AddMilliseconds(configuration.BuildTime),
                LastCollectDate = DateTime.Now.AddMilliseconds(configuration.BuildTime),
                Player = player,
            };

            int id = await _buildingRepository.AddBuildingAsync(building);

            return new BuildingDto()
            {
                Id = id,
                BuildingType = configuration.BuildingType,
                Name = configuration.Name,
                XCoordinate = building.XCoordinate,
                YCoordinate = building.YCoordinate,
                Level = building.Level,
                MaxLevel = configuration.MaxLevel,
                Description = configuration.Description,
                SpritePath = configuration.SpritePath,
                CoinsForBuild = configuration.CoinsForBuild,
                IronsForBuild = configuration.IronsForBuild,
                StonesForBuild = configuration.StonesForBuild,
                WoodsForBuild = configuration.WoodsForBuild,
                ProducedCoins = configuration.ProducedCoins,
                ProducedIrons = configuration.ProducedIrons,
                ProducedStones = configuration.ProducedStones,
                ProducedWoods = configuration.ProducedWoods,
                ExperienceReward = configuration.ExperienceReward,
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
                WoodsForBuild = building.WoodsForBuild
            }).ToList();
        }

        public async Task<UnbuiltBuildingDto> GetNextLevelOfBuildingByIslandAsync(string username, BuildingType buildingType)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            Building building = await _buildingRepository.GetBuildingAsync(username, buildingType);
            BuildingConfigurationDto nextLevelBuilding = await _configurationService.GetBuildingByIslandAsync(player.SelectedIsland, buildingType, building.Level + 1);

            return new UnbuiltBuildingDto()
            {
                BuildingType = nextLevelBuilding.BuildingType,
                Name = nextLevelBuilding.Name,
                Description = nextLevelBuilding.Description,
                SpritePath = nextLevelBuilding.SpritePath,
                CoinsForBuild = nextLevelBuilding.CoinsForBuild,
                IronsForBuild = nextLevelBuilding.IronsForBuild,  
                StonesForBuild = nextLevelBuilding.StonesForBuild,
                WoodsForBuild = nextLevelBuilding.WoodsForBuild   
            };
        }

        public async Task<BuildingDto> UpgradeBuildingAsync(string username, BuildingType type)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            Building building = await _buildingRepository.GetBuildingAsync(username, type);
            BuildingConfigurationDto currentConfiguration = 
                await _configurationService.GetBuildingByIslandAsync(player.SelectedIsland, building.BuildingType, building.Level);

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
            player.Woods -= nextLevelConfiguration.WoodsForBuild;
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
        public async Task<ItemsDto> CollectItemsAsync(string username, BuildingType type)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            Building building = await _buildingRepository.GetBuildingAsync(username, type);
            BuildingConfigurationDto buildingConfiguration = 
                await _configurationService.GetBuildingByIslandAsync(player.SelectedIsland, building.BuildingType, building.Level);

            int ticksFromBuild = 
                    Convert.ToInt32((DateTime.Now - building.BuildDate).TotalMilliseconds / buildingConfiguration.ProductionInterval);
            int ticksBetweenBuildAndLastCollect = 
                    Convert.ToInt32((building.LastCollectDate - building.BuildDate).TotalMilliseconds / buildingConfiguration.ProductionInterval);

            int ticks = ticksFromBuild - ticksBetweenBuildAndLastCollect;

            int producedCoins = 
                ticks * buildingConfiguration.ProducedCoins < buildingConfiguration.MaximumProductionCount ? 
                ticks * buildingConfiguration.ProducedCoins : buildingConfiguration.MaximumProductionCount;
            int producedWoods = 
                ticks * buildingConfiguration.ProducedWoods < buildingConfiguration.MaximumProductionCount ? 
                ticks * buildingConfiguration.ProducedWoods : buildingConfiguration.MaximumProductionCount;
            int producedStones = 
                ticks * buildingConfiguration.ProducedStones < buildingConfiguration.MaximumProductionCount ? 
                ticks * buildingConfiguration.ProducedStones : buildingConfiguration.MaximumProductionCount;
            int producedIrons = 
                ticks * buildingConfiguration.ProducedIrons < buildingConfiguration.MaximumProductionCount ? 
                ticks * buildingConfiguration.ProducedIrons : buildingConfiguration.MaximumProductionCount;

            building.LastCollectDate = DateTime.Now;

            player.Coins += producedCoins;
            player.Woods += producedWoods;
            player.Stones += producedStones;
            player.Irons += producedIrons;

            await _playerRepository.UpdatePlayerAsync(player);
            await _buildingRepository.UpdateBuildingAsync(building);

            return new ItemsDto(producedCoins, producedWoods, producedStones, producedIrons);
        }
    }
}
