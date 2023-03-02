using BLL.Exceptions;
using BLL.Services.ConfigurationService;
using DAL.DTOs;
using DAL.Models.Enums;
using DAL.Repositories.PlayerRepository;

namespace BLL.Services.IslandService
{
    public class IslandService : IIslandService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IConfigurationService _configurationService;

        public IslandService(
            IPlayerRepository playerRepository,
            IConfigurationService gameConfigurationService)
        {
            _playerRepository = playerRepository;
            _configurationService = gameConfigurationService;
        }

        public async Task<IslandDto> GetIslandByUsernameAsync(string username)
        {
            try
            {
                IslandType island = await _playerRepository.GetIslandTypeByUsernameAsync(username);
                return await _configurationService.GetIslandAsync(island);
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
