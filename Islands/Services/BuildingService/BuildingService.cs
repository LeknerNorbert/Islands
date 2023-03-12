using Islands.DTOs;
using Islands.Exceptions;
using Islands.Models;
using Islands.Models.DTOs;
using Islands.Models.Enums;
using Islands.Repositories.BuildingRepository;
using Islands.Repositories.PlayerInformationRepository;
using Islands.Services.IslandService;

namespace Islands.Services.BuildingService
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository buildingRepository;
        private readonly IPlayerRepository playerRepository;
        private readonly IGameConfigurationService gameConfigurationService;

        public BuildingService(
            IGameConfigurationService gameConfigurationService,
            IBuildingRepository buildingRepository,
            IPlayerRepository playerRepository)
        {
            this.gameConfigurationService = gameConfigurationService;
            this.buildingRepository = buildingRepository;
            this.playerRepository = playerRepository;
        }

        public async Task<BuildingDto> AddBuildingAsync(string username, BuildingRequestDto buildingRequest)
        {
            if (await buildingRepository.CheckBuildingIsExist(username, buildingRequest.Type))
            {
                throw new BuildingAlreadyExistException("Building already exist.");
            }

            if (buildingRequest.XCoordinate > 25 || 
                buildingRequest.YCoordinate < 0 || 
                buildingRequest.YCoordinate > 15 || 
                buildingRequest.YCoordinate < 0)
            {
                throw new InvalidCoordinateException("Invalid coordinates.");
            }

            Player player = await playerRepository.GetPlayerByUsernameAsync(username);
            Building building = new()
            {
                Type = buildingRequest.Type,
                XCoordinate = buildingRequest.XCoordinate,
                YCoordinate = buildingRequest.YCoordinate,
                Level = 1,
                BuildDate = DateTime.Now,
                LastCollectDate = DateTime.Now,
                Player = player,
            };

            await buildingRepository.AddBuildingAsync(building);

            BuildingWithDefaultValuesDto buildingConfiguration = await gameConfigurationService.GetBuildingDefaultValueAsync(buildingRequest.Type, 1);
            BuildingDto createdBuilding = new()
            {
                Name = building.Type,
                XCoordinate = building.XCoordinate,
                YCoordinate = building.YCoordinate,
                Level = building.Level,
                MaxLevel = buildingConfiguration.MaxLevel,
                Description = buildingConfiguration.Description,
                ImagePath = buildingConfiguration.ImagePath,
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

            return createdBuilding;
        }
        public async Task<List<BuildingDto>> GetAllBuildingAsync(string username)
        {
            throw new NotImplementedException();
        }
        public async Task<BuildingDto> UpgradeBuildingAsync(string username, BuildingType building)
        {
            throw new NotImplementedException();
        }
        public async Task CollectItemsAsync(string username, BuildingType building)
        {
            throw new NotImplementedException();
        }
    }
}
