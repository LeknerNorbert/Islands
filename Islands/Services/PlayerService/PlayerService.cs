using Islands.DTOs;
using Islands.Exceptions;
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

        public async Task<PlayerDto> GetByUsernameAsync(string username)
        {
            try
            {
                Player player = await _playerRepository.GetPlayerByUsernameAsync(username);

                return new PlayerDto()
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
                    Agility = player.Agility,
                };
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to get player.", ex);
            }
        }

        public async Task AddAsync(string username, IslandType island)
        {
            try
            {
                User user = await _userRepository.GetByUsernameAsync(username);
                SkillsDto defaultSkills = await _gameConfigurationService.GetDefaultSkillsByIslandAsync(island);

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
                    Agility = defaultSkills.Ability,
                    User = user,
                };

                await _playerRepository.AddPlayerAsync(player);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to add player.", ex);
            }
        }

        public async Task UpdateSkillsAsync(string username, SkillsDto skills)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            int currentLevel = _gameConfigurationService.GetLevelByExperience(player.ExperiencePoint);

            int availableSkillPoints = currentLevel - player.Intelligence - player.Strength - player.Agility;
            int updateSkillPointsQuantity = skills.Intelligence + skills.Strength + skills.Ability;

            if (availableSkillPoints >= updateSkillPointsQuantity)
            {
                player.Agility += skills.Ability;
                player.Strength += skills.Strength;
                player.Intelligence += skills.Intelligence;

                await _playerRepository.UpdatePlayerAsync(player);
            }
            else
            {
                throw new InsufficientSkillPointsException("No enough skill point.");
            }
        }
    }
}
