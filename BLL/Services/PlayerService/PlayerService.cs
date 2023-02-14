using BLL.DTOs;
using BLL.Services.IslandService;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.PlayerInformationRepository;
using DAL.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.PlayerInformationService
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGameConfigurationService _gameConfigurationService;

        public PlayerService(IPlayerRepository playerRepository, IUserRepository userRepository, IGameConfigurationService gameConfigurationService)
        {
            _playerRepository = playerRepository;
            _userRepository = userRepository; 
            _gameConfigurationService = gameConfigurationService;
        }

        public PlayerDto GetPlayer(string username)
        {
            Player player = _playerRepository.GetPlayerByUsername(username);

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

        public void CreatePlayer(string username, IslandType island)
        {
            User user = _userRepository.GetUserByUsername(username);
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
            
            _playerRepository.CreatePlayer(player);
        }
    }
}
