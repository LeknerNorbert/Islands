using BLL.Exceptions;
using BLL.Services.ConfigurationService;
using DAL.DTOs;
using DAL.Models;
using DAL.Repositories.NotificationRepository;
using DAL.Repositories.PlayerRepository;

namespace BLL.Services.BattleService
{
    public class BattleService : IBattleService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IPlayerRepository _playerRepository;
        private readonly INotificationRepository _notificationRepository;

        private readonly Random random;

        public BattleService(
            IConfigurationService configurationService,
            IPlayerRepository playerRepository,
            INotificationRepository notificationRepository)
        {
            _configurationService = configurationService;
            _playerRepository = playerRepository;
            _notificationRepository = notificationRepository;

            random = new();
        }

        public async Task<List<EnemyDto>> GetAllEnemiesAsync(string username)
        {
            Player player = await _playerRepository.GetPlayerByUsernameAsync(username);
            int playerLevel = _configurationService.GetLevelByExperience(player.Experience);

            int minLevel = playerLevel < 2 ? 0 : playerLevel - 2;
            int maxLevel = playerLevel > 28 ? 30 : playerLevel + 2;
            int minExperience = _configurationService.GetExperienceByLevel(minLevel);
            int maxExperience = _configurationService.GetExperienceByLevel(maxLevel);

            List<Player> enemyPlayers = await _playerRepository.GetTopSixPlayersByExperience(username, minExperience, maxExperience);
            List<EnemyDto> enemies = new();

            foreach (Player enemyPlayer in enemyPlayers)
            {
                EnemyConfigurationDto configuration = await _configurationService.GetEnemyByIslandAsync(enemyPlayer.SelectedIsland);

                enemies.Add(new EnemyDto()
                {
                    PlayerId = enemyPlayer.Id,
                    Username = enemyPlayer.User.Username,
                    SpritePath = configuration.SpritePath,
                    Level = _configurationService.GetLevelByExperience(enemyPlayer.Experience)
                });
            }

            return enemies;
        }

        public async Task<BattleReportDto> GetBattleReportAsync(string username, int enemyId)
        {
            PlayerForBattleDto player = await _playerRepository.GetPlayerForBattleByUsernameAsync(username);
            PlayerForBattleDto enemy = await _playerRepository.GetPlayerForBattleByIdAsync(enemyId);

            if (player.LastBattleDate > DateTime.Now.AddMinutes(-10))
            {
                throw new BattleNotAllowedException($"Battle is not allowed, date of your last battle is {player.LastBattleDate}");
            }

            int winnerId = 0;
            int lootWoods = 0;
            int lootStones = 0;
            int lootIrons = 0;
            int lootCoins = 0;
            int lootExperience = 0;

            BattleReportDto battleReports = new();
            List<string> battleText = new()
            {
                "Bekerítette az ellenfél csapatait!",
                "Tervei sikeresek voltak!",
                "Megfelelő stratégiát választott!",
                "Kritikus helyet foglalt el!",
                "Az ellenfél rosszul védekezett!",
                "Az ellenség sok embert vesztett!",
                "A meglepetés támadás sikeres volt!",
                "Elkerülhetetlen csapást mért!",
                "Isten megjutalmazott az imádságaidért!",
                "Az egyik embere magával rántott a pokolba sok ellenfelet!"
            };

            bool isBattleOver = false;
            while (isBattleOver != true)
            {
                if (player.Health > 0)
                {

                    int damage = AttackDamage(player.Strength, player.Agility, player.ChurchLevel, player.PracticeRangeLevel);
                    enemy.Health -= damage;
                    int txt = random.Next(0, 9);

                    RoundDto round = new();

                    round.Username = player.Username;
                    round.Message = battleText[txt];
                    round.Damage = damage;
                    round.EnemyReminingHealth = enemy.Health;

                    battleReports.Rounds.Add(round);

                    if (enemy.Health > 0)
                    {
                        int damage2 = AttackDamage(enemy.Strength, enemy.Agility, enemy.ChurchLevel, enemy.PracticeRangeLevel);
                        player.Health -= damage2;
                        int txt2 = random.Next(0, 9);

                        RoundDto round2 = new();

                        round2.Username = enemy.Username;
                        round2.Message = battleText[txt2];
                        round2.Damage = damage2;
                        round2.EnemyReminingHealth = player.Health;

                        battleReports.Rounds.Add(round2);
                    }
                    else
                    {
                        winnerId = player.Id;

                        lootWoods = LootCalc(enemy.Intelligence);
                        lootStones = LootCalc(enemy.Intelligence);
                        lootIrons = LootCalc(enemy.Intelligence);
                        lootCoins = CoinCalc(enemy.Intelligence);
                        lootExperience = CoinCalc(enemy.Intelligence);

                        battleReports.IsWinner = true;
                        battleReports.Woods = lootWoods;
                        battleReports.Stones = lootStones;
                        battleReports.Irons = lootIrons;
                        battleReports.Coins = lootCoins;
                        battleReports.Experience = lootExperience;

                        isBattleOver = true;
                    }
                }
                else
                {
                    winnerId = enemy.Id;

                    lootWoods = LootCalc(enemy.Intelligence);
                    lootStones = LootCalc(enemy.Intelligence);
                    lootIrons = LootCalc(enemy.Intelligence);
                    lootCoins = CoinCalc(enemy.Intelligence);
                    lootExperience = CoinCalc(enemy.Intelligence);

                    battleReports.IsWinner = false;
                    battleReports.Woods = 0;
                    battleReports.Stones = 0;
                    battleReports.Irons = 0;
                    battleReports.Coins = 0;
                    battleReports.Experience = 0;

                    isBattleOver = true;
                }

            }

            Player winnerPlayer = await _playerRepository.GetPlayerByIdAsync(winnerId);

            winnerPlayer.Woods += lootWoods;
            winnerPlayer.Stones += lootStones;
            winnerPlayer.Irons += lootIrons;
            winnerPlayer.Coins += lootCoins;
            winnerPlayer.Experience += lootExperience;

            Notification winnerNotification = new()
            {
                Player = winnerPlayer,
                Title = "Győztes csata",
                Woods = lootWoods,
                Stones = lootStones,
                Irons = lootIrons,
                Coins = lootCoins,
                Experience = lootExperience,
                CreateDate = DateTime.Now
            };

            Player battleStartPlayer = await _playerRepository.GetPlayerByUsernameAsync(username);
            battleStartPlayer.LastBattleDate = DateTime.Now;

            await _playerRepository.UpdatePlayerAsync(battleStartPlayer);
            await _playerRepository.UpdatePlayerAsync(winnerPlayer);
            await _notificationRepository.AddNotificationAsync(winnerNotification);

            return battleReports;
        }

        private int AttackDamage(int str, int agi, int churchLvl, int trainingLvl)
        {
            double critChance = 10 + (agi / 2);
            int crit = random.Next(0, 100);

            if (crit <= critChance)
            {
                if (trainingLvl == 1)
                {
                    return DamageCalc(1.5, str, churchLvl);
                }
                else if (trainingLvl == 2)
                {
                    return DamageCalc(1.75, str, churchLvl);
                }
                else
                {
                    return DamageCalc(2, str, churchLvl);
                }
            }
            else
            {
                return DamageCalc(1, str, churchLvl);
            }

        }

        private int DamageCalc(double multiply, int str, int churchLvl)
        {
            int min = (5 + str);
            int max = (10 + str);
            int randomDamage = random.Next(min, max);
 
            double calculator = randomDamage * (1 + (churchLvl / 10));
            int churchDmg = Convert.ToInt32(Math.Round(calculator));

            double allDmg = Math.Round(churchDmg * multiply);
            int damage = Convert.ToInt32(allDmg);
            return damage;
        }

        private int LootCalc(int intellect)
        {
            int baseLoot = random.Next(50, 100);

            double calculator = baseLoot + (intellect * 5);
            int loot = Convert.ToInt32(Math.Round(calculator));

            return loot;
        }

        private int CoinCalc(int intellect)
        {
            int baseLoot = random.Next(100, 200);

            double calculator = baseLoot + (intellect * 5);
            int coin = Convert.ToInt32(Math.Round(calculator));

            return coin;
        }
    }
}
