using BLL.Exceptions;
using DAL.DTOs;
using DAL.Models;
using DAL.Repositories.NotificationRepository;
using DAL.Repositories.PlayerRepository;

namespace BLL.Services.ExpeditionService
{
    public class ExpeditionService : IExpeditionService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly Random rng;

        public ExpeditionService(
            IPlayerRepository playerRepository, 
            INotificationRepository notificationRepository)
        {
            _playerRepository = playerRepository;
            _notificationRepository = notificationRepository;
            rng = new();
        }

        public async Task<ExpeditionReportDto> GetExpeditionReportAsync(string username, int difficulty)
        {
            Player? player = await _playerRepository.GetPlayerByUsernameAsync(username);

            if (player == null)
            {
                throw new ArgumentException("Player does not exist!");
            }

            if (player.LastExpeditionDate > DateTime.Now.AddMinutes(-1))
            {
                throw new ExpeditionNotAllowedException($"Expedition is not allowed, date of your last expedition is {player.LastExpeditionDate}");
            }

            string message;
            string expTitle;
            int lootWoods = 0;
            int lootStones = 0;
            int lootIrons = 0;
            int lootCoins = 0;
            int lootExperience = 0;

            ExpeditionReportDto expeditionReport = new();

            //Expedíció nyerés szövegek
            List<string> winText = new()
            {
                "A felfedező csapat talált egy barlangot, úgy döntöttek lemennek felderíteni. Egy elágazáshoz értek, amely további 5 utat nyitot meg. Úgy döntöttek, hogy egy csapatban maradnak. Ez jó döntés volt, mivel mindegyik alagútban volt egy ellenfél, amit egyedül nem tudtak volna legyőzni. Ennek köszönhetően sikeres volt az expedíció. A jutalom: ",
                "A felfedező csapat belebotlott egy hajóroncsba. Szerencsére nem volt ellenség a közelben, így könnyedén át tudták kutatni a roncsot. Az alábbi dolgokat, tudták elhozni: ",
                "A felfedező csapat több nap távollét után végre hazaért. Mindenki azt hitte, hogy elvesztek - vagy rosszabb történt velük -, de szerencsére csak az tartott ilyen sokáig, hogy haza tudják hozni az alábbi lootot: ",
                "A felfedezők egy elhagyott épületet találtak. Az egyik szobában egy halott embert találtak egy kulccsal a kezében. A pincében találták meg azt a ládát, amihez a kulcs tartozott. Kinyitva ezeket találták: ",
                "A felfedezők egy kalóz holttestét találták meg a parton egy térképpel. Úgy néz ki, hogy a szigeten egy kincs volt elrejtve, de nem találta meg időben. Viszont a csapat egy tagja felismerte a helyet, ahol a kincs lehet. Sikeresen megtalálták a helyet, majd kiásva az alábbiakat találta a csapat: ",
                "A felfedezedő csapatra rátámadt egy ellenséges csapat. Nagy harc alatt az összes ellenséget legyőzték, de egy ember súlyosan megsérült. Halála nem volt hiába való, az alábbiakat szerezte az ellenségtől: ",
                "A felfedező csapat egy tisztáson belebotlott egy mágikus szoborba, amelyből előjött egy női szellem. Azt mondta nekik, hogy teljesíti a kivánságaikat, de cserébe az egyiküknek a lelkét kéri. A vezető hősiesen feláldozta magát, hogy a szigeten élő embereknek jobb legyen, jutalmad: "
            };

            //Expedíció vesztés szövegek
            List<string> loseText = new()
            {
                "A felfedező csapatra rátámadt egy farkas falka, szét marcangoltak mindenkit. Az expedíció így sikertelen volt.",
                "A felderítő csapat belépett egy barlangba. Egy elágazáshoz értek, majd úgy döntöttek, hogy szétválnak. Sajnos mindegyikük egy náluk erősebb ellenféllel kerültek össze. Ha együtt maradtak volna, akkor túlélik. Így sajnos a felderítés sikertelen volt.",
                "A felfedezők egy ösvényre értek, ahol megtámadta őket egy ellenséges csapat. Olyan hirtelen jött a támadás, hogy nem tudták megfékezni azt. Mindegyikük életét vesztette. A felfedezés sikertelen volt.",
                "Az expedícióra küldött csapat egy mágikus kőre talált, ahonnan egy női szellem bújt elő. Azt ígérte nekik, hogy megkapják, amit csak akarnak, viszont ahhoz meg kell csókolniuk a követ egyszerre. De amikor ezt a csapat megtette, valójában a lelküket szívta ki a kő. Az expedíció sikertelen volt.",
                "A felfedező csapat nagyszerű helyet talált, tele volt minden alapanyaggal és arannyal. Ám nem tudták, hogy meg van a hely átkozva. Egy idő után elkezdték egymást gyanusítani, hogy meg akarják lopni a másikat. Végül mindenki egymásra támadt és megölték egymást. A felfedezés sikertelen volt.",
                "A felfedező csapat kifogyott a vízből és élelemből. Elkezdtek különböző növényeket enni és furcsa vízforrásokból ittak. Mindannyian szenvedések közt haltak meg mérgezésben. Nem volt sikeres az expedíció."
            };

            int winOrLose = rng.Next(0, 9) + 1;

            if (difficulty == 1)
            {
                if (winOrLose <= 9) //90% esély
                {
                    int win = rng.Next(0, 6);
                    message = winText[win];
                    expTitle = "Sikeres expedíció";
                    lootWoods = LootCalc(player.Intelligence, rng, difficulty);
                    lootStones = LootCalc(player.Intelligence, rng, difficulty);
                    lootIrons = LootCalc(player.Intelligence, rng, difficulty);
                    lootCoins = CoinCalc(player.Intelligence, rng, difficulty);
                    lootExperience = CoinCalc(player.Intelligence, rng, difficulty);

                    expeditionReport.Message = message;
                    expeditionReport.Title = expTitle;
                    expeditionReport.IsSuccess = true;
                    expeditionReport.Woods = lootWoods;
                    expeditionReport.Stones = lootStones;
                    expeditionReport.Irons = lootIrons;
                    expeditionReport.Coins = lootCoins;
                    expeditionReport.Experience = lootExperience;
                }
                else
                {
                    int lose = rng.Next(0, 5);
                    message = loseText[lose];
                    expTitle = "Sikertelen expedíció";

                    expeditionReport.Message = message;
                    expeditionReport.Title = expTitle;
                    expeditionReport.IsSuccess = false;
                    expeditionReport.Woods = 0;
                    expeditionReport.Stones = 0;
                    expeditionReport.Irons = 0;
                    expeditionReport.Coins = 0;
                    expeditionReport.Experience = 0;
                }
            }
            else if (difficulty == 2)
            {
                if (winOrLose <= 7) //70% esély
                {
                    int win = rng.Next(0, 6);
                    message = winText[win];
                    expTitle = "Sikeres expedíció";
                    lootWoods = LootCalc(player.Intelligence, rng, difficulty);
                    lootStones = LootCalc(player.Intelligence, rng, difficulty);
                    lootIrons = LootCalc(player.Intelligence, rng, difficulty);
                    lootCoins = CoinCalc(player.Intelligence, rng, difficulty);
                    lootExperience = CoinCalc(player.Intelligence, rng, difficulty);

                    expeditionReport.Message = message;
                    expeditionReport.Title = expTitle;
                    expeditionReport.IsSuccess = true;
                    expeditionReport.Woods = lootWoods;
                    expeditionReport.Stones = lootStones;
                    expeditionReport.Irons = lootIrons;
                    expeditionReport.Coins = lootCoins;
                    expeditionReport.Experience = lootExperience;
                }
                else
                {
                    int lose = rng.Next(0, 5);
                    message = loseText[lose];
                    expTitle = "Sikertelen expedíció";

                    expeditionReport.Message = message;
                    expeditionReport.Title = expTitle;
                    expeditionReport.IsSuccess = false;
                    expeditionReport.Woods = 0;
                    expeditionReport.Stones = 0;
                    expeditionReport.Irons = 0;
                    expeditionReport.Coins = 0;
                    expeditionReport.Experience = 0;
                }
            }
            else
            {
                if (winOrLose <= 5) // 50% esély
                {
                    int win = rng.Next(0, 6);
                    message = winText[win];
                    expTitle = "Sikeres expedíció";
                    lootWoods = LootCalc(player.Intelligence, rng, difficulty);
                    lootStones = LootCalc(player.Intelligence, rng, difficulty);
                    lootIrons = LootCalc(player.Intelligence, rng, difficulty);
                    lootCoins = CoinCalc(player.Intelligence, rng, difficulty);
                    lootExperience = CoinCalc(player.Intelligence, rng, difficulty);

                    expeditionReport.Message = message;
                    expeditionReport.Title = expTitle;
                    expeditionReport.IsSuccess = true;
                    expeditionReport.Woods = lootWoods;
                    expeditionReport.Stones = lootStones;
                    expeditionReport.Irons = lootIrons;
                    expeditionReport.Coins = lootCoins;
                    expeditionReport.Experience = lootExperience;
                }
                else
                {
                    int lose = rng.Next(0, 5);
                    message = loseText[lose];
                    expTitle = "Sikertelen expedíció";

                    expeditionReport.Message = message;
                    expeditionReport.Title = expTitle;
                    expeditionReport.IsSuccess = false;
                    expeditionReport.Woods = 0;
                    expeditionReport.Stones = 0;
                    expeditionReport.Irons = 0;
                    expeditionReport.Coins = 0;
                    expeditionReport.Experience = 0;
                }
            }


            Notification expeditionNotification = new()
            {
                Player = player,
                Title = expTitle,
                Message = message,
                Woods = lootWoods,
                Stones = lootStones,
                Irons = lootIrons,
                Coins = lootCoins,
                Experience = lootExperience,
                CreateDate = DateTime.Now
            };

            player.Woods += lootWoods;
            player.Stones += lootStones;
            player.Irons += lootIrons;
            player.Coins += lootCoins;
            player.Experience += lootExperience;
            player.LastExpeditionDate = DateTime.Now;

            await _playerRepository.UpdatePlayerAsync(player);
            await _notificationRepository.AddNotificationAsync(expeditionNotification);

            return expeditionReport;
        }

        static int LootCalc(int intellect, Random rngLoot, int difficulty)
        {
            int baseLoot = rngLoot.Next(50, 100);
            double calculator = baseLoot + (intellect * 5);
            int loot = Convert.ToInt32(Math.Round(calculator));

            if (difficulty == 1)
            {
                return loot - 50;
            }
            else if (difficulty == 2)
            {
                return loot;
            }
            else
            {
                return loot + 50;
            }
        }


        static int CoinCalc(int intellect, Random rngCoin, int difficulty)
        {
            int baseLoot = rngCoin.Next(100, 200);
            double calculator = baseLoot + (intellect * 5);
            int coin = Convert.ToInt32(Math.Round(calculator));
            if (difficulty == 1)
            {
                return coin - 100;
            }
            else if (difficulty == 2)
            {
                return coin;
            }
            else
            {
                return coin + 100;
            }

        }
    }
}
