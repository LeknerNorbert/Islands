using Islands.Exceptions;
using Islands.Models;
using Islands.Models.DTOs;
using Islands.Repositories.NotificationRepository;
using Islands.Repositories.PlayerInformationRepository;

namespace Islands.Services.BattleService
{
    public class BattleService : IBattleService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly INotificationRepository notificationRepository;
        private readonly Random random;

        public BattleService(IPlayerRepository playerRepository, INotificationRepository notificationRepository)
        {
            this.playerRepository = playerRepository;
            this.notificationRepository = notificationRepository;
            random = new();
        }

        public async Task<BattleResultDto> GetBattleResultAsync(string username, int enemyId)
        {
            PlayerForBattleDto player = await playerRepository.GetPlayerForBattleByUsernameAsync(username);
            PlayerForBattleDto enemy = await playerRepository.GetPlayerForBattleByIdAsync(enemyId);
            
            int winnerId = 0;
            int lootWood = 0;
            int lootStone = 0;
            int lootIron = 0;
            int lootCoin = 0;
            int lootExperiencePoints = 0; 

            BattleResultDto battleResult = new();
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

                    BattleReportDto battleReport = new();

                    battleReport.Username = player.Username;
                    battleReport.Message = battleText[txt];
                    battleReport.Damage = damage;
                    battleReport.EnemyReminingHealth = enemy.Health;

                    battleResult.Reports.Add(battleReport);

                    if (enemy.Health > 0)
                    {
                        int damage2 = AttackDamage(enemy.Strength, enemy.Agility, enemy.ChurchLevel, enemy.PracticeRangeLevel);
                        player.Health -= damage2;
                        int txt2 = random.Next(0, 9);

                        BattleReportDto battleReport2 = new();

                        battleReport2.Username = enemy.Username;
                        battleReport2.Message = battleText[txt2];
                        battleReport2.Damage = damage2;
                        battleReport2.EnemyReminingHealth = player.Health;

                        battleResult.Reports.Add(battleReport2);
                    }
                    else
                    {
                        winnerId = player.Id;
                        lootWood = LootCalc(enemy.Intelligence);
                        lootStone = LootCalc(enemy.Intelligence);
                        lootIron = LootCalc(enemy.Intelligence);
                        lootCoin = CoinCalc(enemy.Intelligence);
                        lootExperiencePoints = CoinCalc(enemy.Intelligence);

                        battleResult.IsWinner = true;
                        battleResult.Woods = lootWood;
                        battleResult.Stones = lootStone;
                        battleResult.Irons = lootIron;
                        battleResult.Coins = lootCoin;
                        battleResult.ExperiencePoints = lootExperiencePoints;

                        isBattleOver = true;
                    }
                }
                else
                {
                    winnerId = enemy.Id;
                    lootWood = LootCalc(enemy.Intelligence);
                    lootStone = LootCalc(enemy.Intelligence);
                    lootIron = LootCalc(enemy.Intelligence);
                    lootCoin = CoinCalc(enemy.Intelligence);
                    lootExperiencePoints = CoinCalc(enemy.Intelligence);

                    battleResult.IsWinner = false;
                    battleResult.Woods = 0;
                    battleResult.Stones = 0;
                    battleResult.Irons = 0;
                    battleResult.Coins = 0;
                    battleResult.ExperiencePoints = 0;

                    isBattleOver = true;
                }

            }

            Player winnerPlayer = await playerRepository.GetPlayerByIdAsync(winnerId);

            winnerPlayer.Woods += lootWood;
            winnerPlayer.Stones += lootStone;
            winnerPlayer.Irons += lootIron;
            winnerPlayer.Coins += lootCoin;
            winnerPlayer.ExperiencePoint += lootExperiencePoints;

            Notification winnerNotification = new()
            {
                Player = winnerPlayer,
                Title = "Győztes csata",
                Wood = lootWood,
                Stone = lootStone,
                Iron = lootIron,
                Coin = lootCoin,
                ExperiencePoint = lootExperiencePoints,
                Date = DateTime.Now
            };

            await playerRepository.UpdatePlayerAsync(winnerPlayer);
            await notificationRepository.AddNotificationAsync(winnerNotification);

            return battleResult;
        }

        private int AttackDamage(int str, int agi, int churchLvl, int trainingLvl)
        {
            //random plusz templom, gyakorlat, erő és ügyesség konvertálása számmá
            //strenght az 1 az 1ben plusz damage-t ad
            //agility az a kritikus találat esélyét növeli (az alap az 10% és az össz agilitynak a 50%-a hozzáadódik. pl 10 agilty az 5% crit, 20/2 = 10% crit stb.
            //church level plusz sebzés %os alapon

            double critChance = 10 + (agi / 2);
            int crit = random.Next(0, 100);

            if (crit <= critChance)
            {
                //crit ág
                //training level az a crit plusz sebzését adja meg pl 50% 1-es szinten, 75% 2-es szinten és duplázza a sebzést 3-as szinten
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
            //templomos dmg növelés
            double calculator = randomDamage * (1 + (churchLvl / 10));
            int churchDmg = Convert.ToInt32(Math.Round(calculator));

            double allDmg = Math.Round(churchDmg * multiply);
            int damage = Convert.ToInt32(allDmg);
            return damage;
        }

        private int LootCalc(int intellect)
        {
            //intellect növeli a loot mennyiséget az alap intelligencia érték ötszörösével. pl 10nél 50 + anyagot kap, 30nál 150et stb.
            //random alap loot
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
