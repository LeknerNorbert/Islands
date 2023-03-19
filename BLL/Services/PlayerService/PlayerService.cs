﻿using BLL.Exceptions;
using BLL.Services.ConfigurationService;
using DAL.DTOs;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.NotificationRepository;
using DAL.Repositories.PlayerRepository;
using DAL.Repositories.UserRepository;

namespace BLL.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfigurationService _configurationService;
        private readonly INotificationRepository _notificationRepository;

        public PlayerService(
            IPlayerRepository playerRepository, 
            IUserRepository userRepository, 
            IConfigurationService configurationService,
            INotificationRepository notificationRepository)
        {
            _playerRepository = playerRepository;
            _userRepository = userRepository; 
            _configurationService = configurationService;
            _notificationRepository = notificationRepository;
        }

        public async Task<PlayerDto> AddPlayerAsync(string username, IslandType name)
        {
            User user = await _userRepository.GetUserByUsernameAsync(username);
            SkillsDto defaultSkills = await _configurationService.GetDefaultSkillsByIslandAsync(name);

            Player player = new()
            {
                Experience = 100,
                Coins = 100,
                Woods = 100,
                Stones = 100,
                Irons = 100,
                SelectedIsland = name,
                LastBattleDate = DateTime.MinValue,
                LastExpeditionDate = DateTime.MinValue,
                Strength = defaultSkills.Strength,
                Intelligence = defaultSkills.Intelligence,
                Agility = defaultSkills.Agility,
                User = user,
            };

            Player createdPlayer = await _playerRepository.AddPlayerAsync(player);

            Notification notification = new()
            {
                Title = "Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!",
                Message = @"Kedves Islander,

                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.
                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.
                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!
                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!

                Üdvözlettel,
                Az Islanders csapata",
                Experience = 100,
                Coins = 100,
                Woods = 100,
                Stones = 100,
                Irons = 100,
                CreateDate = DateTime.Now,
                Player = createdPlayer
            };

            await _notificationRepository.AddNotificationAsync(notification);

            return new PlayerDto()
            {
                Id = createdPlayer.Id,
                Experience = createdPlayer.Experience,
                Coins = createdPlayer.Coins,
                Woods = createdPlayer.Woods,
                Stones = createdPlayer.Stones,
                Irons = createdPlayer.Irons,
                SelectedIsland = name.ToString(),
                LastBattleDate = createdPlayer.LastBattleDate,
                LastExpeditionDate = createdPlayer.LastExpeditionDate,
                Strength = createdPlayer.Strength,
                Intelligence = createdPlayer.Intelligence,
                Agility = createdPlayer.Agility,
            };
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
                SelectedIsland = player.SelectedIsland.ToString(),
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
