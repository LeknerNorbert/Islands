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
                Player player = await _playerRepository.GetByUsernameAsync(username);

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
                    Ability = player.Ability,
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
                SkillsDto defaultSkills = _gameConfigurationService.GetDefaultSkillsByIsland(island);

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

                await _playerRepository.AddAsync(player);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to add player.", ex);
            }
        }

        public async Task UpdateSkillsAsync(string username, SkillsDto skills)
        {
            try
            {
                Player player = await _playerRepository.GetByUsernameAsync(username);
                int skillPointsByLevel = _gameConfigurationService.GetSkillPointsByLevel(player.ExperiencePoint);

                int availableSkillPoints = skillPointsByLevel - player.Intelligence - player.Strength - player.Ability;
                int allSkillPoints = skills.Intelligence + skills.Strength + skills.Ability;

                if (availableSkillPoints >= allSkillPoints)
                {
                    player.Ability += skills.Ability;
                    player.Strength += skills.Strength;
                    player.Intelligence += skills.Intelligence;

                    await _playerRepository.UpdateAsync(player);
                }
                else
                {
                    throw new InsufficientSkillPointsException();
                }
            }
            catch (Exception ex)
            {
                if (ex is InsufficientSkillPointsException)
                {
                    throw new InsufficientSkillPointsException("No enough skill points.");    
                }

                throw new ServiceException("Failed to update player.", ex);
            }
        }
    }
}
