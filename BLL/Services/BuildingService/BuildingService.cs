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
            if (await _buildingRepository.CheckBuildingIsExist(username, buildingRequest.Type))
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
            BuildingConfigurationDto buildingConfiguration =
                await _configurationService.GetBuildingAsync(buildingRequest.Type, 1);

            Building building = new()
            {
                Type = buildingRequest.Type,
                XCoordinate = buildingRequest.XCoordinate,
                YCoordinate = buildingRequest.YCoordinate,
                Level = 1,
                BuildDate = DateTime.Now.AddMilliseconds(buildingConfiguration.BuildTime),
                LastCollectDate = DateTime.Now,
                Player = player,
            };

            await _buildingRepository.AddBuildingAsync(building);

            return new BuildingDto()
            {
                Name = building.Type.ToString(),
                XCoordinate = building.XCoordinate,
                YCoordinate = building.YCoordinate,
                Level = building.Level,
                MaxLevel = buildingConfiguration.MaxLevel,
                Description = buildingConfiguration.Description,
                SpritePath = buildingConfiguration.SpritePath,
                CoinsForUpdate = buildingConfiguration.CoinsForUpdate,
                IronsForUpdate = buildingConfiguration.IronsForUpdate,
                StonesForUpdate = buildingConfiguration.StonesForUpdate,
                WoodsForUpdate = buildingConfiguration.WoodsForUpdate,
                ProducedCoins = buildingConfiguration.ProducedCoins,
                ProducedIrons = buildingConfiguration.ProducedIrons,
                ProducedStones = buildingConfiguration.ProducedStones,
                ProducedWoods = buildingConfiguration.ProducedWoods,
                ExperienceReward = buildingConfiguration.ExperienceReward,
                BuildDate = building.BuildDate,
                LastCollectDate = building.LastCollectDate
            };
        }
        public async Task<List<BuildingDto>> GetAllBuildingAsync(string username)
        {
            List<Building> buildings = await _buildingRepository.GetAllBuildingByUsernameAsync(username);
            List<BuildingDto> buildingsWithConfiguration = new();

            foreach (Building building in buildings)
            {
                BuildingConfigurationDto configuration =
                    await _configurationService.GetBuildingAsync(building.Type, building.Level);

                buildingsWithConfiguration.Add(new BuildingDto
                {
                    Name = building.Type.ToString(),
                    XCoordinate = building.XCoordinate,
                    YCoordinate = building.YCoordinate,
                    Level = building.Level,
                    Description = configuration.Description,
                    SpritePath = configuration.SpritePath,
                    CoinsForUpdate = configuration.CoinsForUpdate,
                    IronsForUpdate = configuration.IronsForUpdate,
                    StonesForUpdate = configuration.StonesForUpdate,
                    WoodsForUpdate = configuration.WoodsForUpdate,
                    ProductionInterval = configuration.ProductionInterval,
                    ProducedCoins = configuration.ProducedCoins,
                    ProducedIrons = configuration.ProducedIrons,
                    ProducedStones = configuration.ProducedStones,
                    ProducedWoods = configuration.ProducedWoods,
                    ExperienceReward = configuration.ExperienceReward,
                    BuildDate = building.BuildDate,
                    LastCollectDate = building.LastCollectDate
                });
            }

            return buildingsWithConfiguration;
        }

        public async Task<List<UnconstructedBuildingDto>> GetAllUnconstructedBuildingAsync()
        {
            return await _configurationService.GetAllUnconstructedBuildingAsync();
        }

        public async Task<BuildingDto> UpgradeBuildingAsync(string username, BuildingType type)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            Building building = await _buildingRepository.GetBuildingAsync(username, type);
            BuildingConfigurationDto currentConfiguration = await _configurationService.GetBuildingAsync(building.Type, building.Level);

            if (currentConfiguration.MaxLevel <= building.Level)
            {
                throw new MaxLevelReachedException("Failed to upgrade, max level reached.");
            }

            if (player.Coins < currentConfiguration.CoinsForUpdate ||
                player.Woods < currentConfiguration.WoodsForUpdate ||
                player.Stones < currentConfiguration.StonesForUpdate ||
                player.Irons < currentConfiguration.IronsForUpdate)
            {
                throw new NotEnoughItemsException("There are no enough items for upgrade.");
            }

            building.Level += 1;
            building.LastCollectDate = DateTime.Now;
            building.BuildDate = DateTime.Now.AddMilliseconds(currentConfiguration.BuildTime);

            player.Coins -= currentConfiguration.CoinsForUpdate;
            player.Woods -= currentConfiguration.WoodsForUpdate;
            player.Stones -= currentConfiguration.StonesForUpdate;
            player.Woods -= currentConfiguration.WoodsForUpdate;

            await _buildingRepository.UpdateBuildingAsync(building);
            await _playerRepository.UpdatePlayerAsync(player);

            BuildingConfigurationDto nextLevelConfiguration = await _configurationService.GetBuildingAsync(building.Type, building.Level);

            return new BuildingDto()
            {
                Name = building.Type.ToString(),
                XCoordinate = building.XCoordinate,
                YCoordinate = building.YCoordinate,
                Level = building.Level,
                MaxLevel = nextLevelConfiguration.MaxLevel,
                Description = nextLevelConfiguration.Description,
                SpritePath = nextLevelConfiguration.SpritePath,
                CoinsForUpdate = nextLevelConfiguration.CoinsForUpdate,
                IronsForUpdate = nextLevelConfiguration.IronsForUpdate,
                StonesForUpdate = nextLevelConfiguration.StonesForUpdate,
                WoodsForUpdate = nextLevelConfiguration.WoodsForUpdate,
                ProducedCoins = nextLevelConfiguration.ProducedCoins,
                ProducedIrons = nextLevelConfiguration.ProducedIrons,
                ProducedStones = nextLevelConfiguration.ProducedStones,
                ProducedWoods = nextLevelConfiguration.ProducedWoods,
                ExperienceReward = nextLevelConfiguration.ExperienceReward,
                BuildDate = building.BuildDate,
                LastCollectDate = building.LastCollectDate
            };
        }
        public async Task<ItemsDto> CollectItemsAsync(string username, BuildingType type)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            Building building = await _buildingRepository.GetBuildingAsync(username, type);
            BuildingConfigurationDto configuration = await _configurationService.GetBuildingAsync(building.Type, building.Level);

            int ticks = Convert.ToInt32((DateTime.Now - building.LastCollectDate).TotalMilliseconds / configuration.ProductionInterval);

            int coins = ticks * configuration.ProducedCoins < 150 ? ticks * configuration.ProducedCoins : 150;
            int woods = ticks * configuration.ProducedWoods < 150 ? ticks * configuration.ProducedWoods : 150;
            int stones = ticks * configuration.ProducedStones < 150 ? ticks * configuration.ProducedStones : 150;
            int irons = ticks * configuration.ProducedIrons < 150 ? ticks * configuration.ProducedIrons : 150;

            building.LastCollectDate = DateTime.Now;

            player.Coins += coins;
            player.Woods += woods;
            player.Stones += stones;
            player.Irons += irons;

            await _playerRepository.UpdatePlayerAsync(player);
            await _buildingRepository.UpdateBuildingAsync(building);

            return new ItemsDto(coins, woods, stones, irons);
        }
    }
}
