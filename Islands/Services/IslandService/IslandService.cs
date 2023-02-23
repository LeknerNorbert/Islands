using Islands.DTOs;
using Islands.Exceptions;
using Islands.Models.Enums;
using Islands.Repositories.PlayerInformationRepository;

namespace Islands.Services.IslandService
{
    public class IslandService : IIslandService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IGameConfigurationService gameConfigurationService;

        public IslandService(
            IPlayerRepository playerRepository, 
            IGameConfigurationService gameConfigurationService)
        {
            this.playerRepository = playerRepository;
            this.gameConfigurationService = gameConfigurationService;   
        }

        public async Task<IslandDto> GetIslandByUsernameAsync(string username)
        {
            try
            {
                IslandType islandType = await playerRepository.GetIslandTypeByUsernameAsync(username);
                return await gameConfigurationService.GetIslandAsync(islandType);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException(); 
            }
            catch (Exception)
            {
                throw new EntityNotFoundException("There is no player associated with this user.");
            }

        }
    }
}
