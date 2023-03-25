using BLL.Exceptions;
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

        public async Task<PlayerDto> AddPlayerAsync(string username, IslandType islandType)
        {
            User user = await _userRepository.GetUserByUsernameAsync(username);
            SkillsDto defaultSkills = await _configurationService.GetDefaultSkillsByIslandAsync(islandType);

            Player player = new()
            {
                Experience = 100,
                Coins = 100,
                Woods = 100,
                Stones = 100,
                Irons = 100,
                SelectedIsland = islandType,
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
                SelectedIsland = islandType.ToString(),
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
            ProfileImageConfigurationDto profileImage = await _configurationService.GetProfileImageByIslandAsync(player.SelectedIsland);

            return new PlayerDto()
            {
                Id = player.Id,
                Experience = player.Experience,
                Coins = player.Coins,
                Woods = player.Woods,
                Stones = player.Stones,
                Irons = player.Irons,
                SelectedIsland = player.SelectedIsland.ToString(),
                ProfileImagePath = profileImage.ImagePath,
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
            SkillsDto maximumSkillPoints = await _configurationService.GetMaximumSkillPointsAsync();
            SkillsDto defaultSkills = await _configurationService.GetDefaultSkillsByIslandAsync(player.SelectedIsland);

            int allSkillPointsInCurrentLevel = defaultSkills.Strength + defaultSkills.Intelligence + defaultSkills.Agility + currentLevel * 3;
            int unusedSkillPoints = allSkillPointsInCurrentLevel - player.Strength - player.Intelligence - player.Agility;

            if (maximumSkillPoints.Strength < player.Strength + skills.Strength || 
                maximumSkillPoints.Intelligence < player.Intelligence + skills.Intelligence ||
                maximumSkillPoints.Agility < player.Agility + skills.Agility)
            {
                throw new NotEnoughSkillPointsException("Maximum skill points reached.");
            }

            if (unusedSkillPoints >= skills.Strength + skills.Intelligence + skills.Agility)
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
        public async Task UpdateItemsAsync(string username, ItemsDto items)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);

            if (player.Coins + items.Coins < 0)
            {
                throw new NotEnoughItemsException("No enough items.");
            }

            player.Coins += items.Coins;
            player.Woods += player.Woods;
            player.Stones += player.Stones;
            player.Irons += player.Irons;

            await _playerRepository.UpdatePlayerAsync(player);
        }
    }
}
