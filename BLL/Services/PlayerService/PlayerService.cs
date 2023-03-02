using BLL.Exceptions;
using BLL.Services.ConfigurationService;
using DAL.DTOs;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.PlayerRepository;
using DAL.Repositories.UserRepository;

namespace BLL.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfigurationService _configurationService;

        public PlayerService(
            IPlayerRepository playerRepository, 
            IUserRepository userRepository, 
            IConfigurationService configurationService)
        {
            _playerRepository = playerRepository;
            _userRepository = userRepository; 
            _configurationService = configurationService;
        }

        public async Task AddPlayerAsync(string username, IslandType name)
        {
            User user = await _userRepository.GetUserByUsernameAsync(username);
            SkillsDto defaultSkills = await _configurationService.GetDefaultSkillsByIslandAsync(name);

            Player player = new()
            {
                Experience = 0,
                Coins = 0,
                Woods = 0,
                Stones = 0,
                Irons = 0,
                SelectedIsland = name,
                LastBattleDate = DateTime.MinValue,
                LastExpeditionDate = DateTime.MinValue,
                Strength = defaultSkills.Strength,
                Intelligence = defaultSkills.Intelligence,
                Agility = defaultSkills.Agility,
                User = user,
            };

            await _playerRepository.AddPlayerAsync(player);
        }

        public async Task<PlayerDto> GetPlayerByUsernameAsync(string username)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);

            return new PlayerDto()
            {
                Id = player.Id,
                Experience = player.Experience,
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

        public async Task UpdateSkillsAsync(string username, SkillsDto skills)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            int currentLevel = _configurationService.GetLevelByExperience(player.Experience);

            int availableSkillPoints = currentLevel - player.Intelligence - player.Strength - player.Agility;
            int updateSkillPointsQuantity = skills.Intelligence + skills.Strength + skills.Agility;

            if (availableSkillPoints >= updateSkillPointsQuantity)
            {
                player.Agility += skills.Agility;
                player.Strength += skills.Strength;
                player.Intelligence += skills.Intelligence;

                await _playerRepository.UpdatePlayerAsync(player);
            }
            else
            {
                throw new NotEnoughSkillPointsException("No enough skill points.");
            }
        }
    }
}
