using Islands.DTOs;
using Islands.Models;
using Islands.Models.Enums;
using Islands.Repositories.PlayerInformationRepository;
using Islands.Repositories.UserRepository;
using Islands.Services.IslandService;

namespace Islands.Services.PlayerInformationService
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGameConfigurationService _gameConfigurationService;

        public PlayerService(
            IPlayerRepository playerRepository, 
            IUserRepository userRepository, 
            IGameConfigurationService gameConfigurationService)
        {
            _playerRepository = playerRepository;
            _userRepository = userRepository; 
            _gameConfigurationService = gameConfigurationService;
        }

        public PlayerDTO GetPlayer(string username)
        {
            Player player = _playerRepository.GetPlayerByUsername(username);

            return new PlayerDTO()
            {
                Id = player.Id,
                ExperiencePoint = player.ExperiencePoint,
                Coins = player.Coins,
                Woods = player.Woods,
                Stones = player.Stones,
                Irons = player.Irons,
                SelectedIsland = player.SelectedIsland,
                LastExpeditionDate = player.LastExpeditionDate,
                LastBattleDate = player.LastBattleDate,
                Strength = player.Strength,
                Intelligence = player.Intelligence,
                Ability = player.Ability,
            };
        }

        public void CreatePlayer(string username, IslandType island)
        {
            User user = _userRepository.GetByUsernameAsync(username);
            SkillsDTO defaultSkills = _gameConfigurationService.GetDefaultSkillsByIsland(island);

            Player player = new()
            {
                ExperiencePoint = 0,
                Coins = 0,
                Woods = 0,
                Stones = 0,
                Irons = 0,
                SelectedIsland = island,
                LastBattleDate = DateTime.MinValue,
                LastExpeditionDate = DateTime.MinValue,
                Strength = defaultSkills.Strength,
                Intelligence = defaultSkills.Intelligence,
                Ability = defaultSkills.Ability,
                User = user,
            };
            
            _playerRepository.CreatePlayer(player);
        }

        public void UpdateSkillPoints(string username, SkillsDTO skills)
        {
            Player player = _playerRepository.GetPlayerByUsername(username);
        }
    }
}
