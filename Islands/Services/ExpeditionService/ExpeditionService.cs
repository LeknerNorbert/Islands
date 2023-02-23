using Islands.Models.DTOs;
using Islands.Models;
using Islands.Repositories.NotificationRepository;
using Islands.Repositories.PlayerInformationRepository;

namespace Islands.Services.ExpeditionService
{
    public class ExpeditionService : IExpeditionService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly INotificationRepository notificationRepository;
        private readonly Random rng;

        public ExpeditionService(IPlayerRepository playerRepository, INotificationRepository notificationRepository)
        {
            this.playerRepository = playerRepository;
            this.notificationRepository = notificationRepository;
            rng = new();
        }

        public async Task<ExpeditionResultDto> GetExpeditionResultAsync(int id, int difficultyId)
        {
            Player? player = await playerRepository.GetPlayerByIdAsync(id);

            if  (player == null)
            {
                throw new ArgumentException("Nem létező játékos!");
            }

            string message = "";
            string expTitle = "";
            int lootWood = 0;
            int lootStone = 0;
            int lootIron = 0;
            int lootCoin = 0;
            int lootExperiencePoints = 0;


            ExpeditionResultDto expeditionResult = new();


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


            int difficulty = difficultyId; 
            int winOrLose = rng.Next(0, 9) + 1;


            if (difficulty == 1)
            {
                if (winOrLose <= 9) //90% esély
                {
                    int win = rng.Next(0, 6);
                    message = winText[win];
                    expTitle = "Sikeres expedíció";
                    lootWood = LootCalc(player.Intelligence, rng, difficulty);
                    lootStone = LootCalc(player.Intelligence, rng, difficulty);
                    lootIron = LootCalc(player.Intelligence, rng, difficulty);
                    lootCoin = CoinCalc(player.Intelligence, rng, difficulty);
                    lootExperiencePoints = CoinCalc(player.Intelligence, rng, difficulty);

                    expeditionResult.Message = message;
                    expeditionResult.Title = expTitle;
                    expeditionResult.IsSuccess = true;
                    expeditionResult.Woods = lootWood;
                    expeditionResult.Stones = lootStone;
                    expeditionResult.Irons = lootIron;
                    expeditionResult.Coins = lootCoin;
                    expeditionResult.ExperiencePoints = lootExperiencePoints;
                }
                else
                {
                    int lose = rng.Next(0, 5);
                    message = loseText[lose];
                    expTitle = "Sikertelen expedíció";

                    expeditionResult.Message = message;
                    expeditionResult.Title = expTitle;
                    expeditionResult.IsSuccess = false;
                    expeditionResult.Woods = 0;
                    expeditionResult.Stones = 0;
                    expeditionResult.Irons = 0;
                    expeditionResult.Coins = 0;
                    expeditionResult.ExperiencePoints = 0;
                }
            }
            else if (difficulty == 2)
            {
                if (winOrLose <= 7) //70% esély
                {
                    int win = rng.Next(0, 6);
                    message = winText[win];
                    expTitle = "Sikeres expedíció";
                    lootWood = LootCalc(player.Intelligence, rng, difficulty);
                    lootStone = LootCalc(player.Intelligence, rng, difficulty);
                    lootIron = LootCalc(player.Intelligence, rng, difficulty);
                    lootCoin = CoinCalc(player.Intelligence, rng, difficulty);
                    lootExperiencePoints = CoinCalc(player.Intelligence, rng, difficulty);

                    expeditionResult.Message = message;
                    expeditionResult.Title = expTitle;
                    expeditionResult.IsSuccess = true;
                    expeditionResult.Woods = lootWood;
                    expeditionResult.Stones = lootStone;
                    expeditionResult.Irons = lootIron;
                    expeditionResult.Coins = lootCoin;
                    expeditionResult.ExperiencePoints = lootExperiencePoints;
                }
                else
                {
                    int lose = rng.Next(0, 5);
                    message = loseText[lose];
                    expTitle = "Sikertelen expedíció";

                    expeditionResult.Message = message;
                    expeditionResult.Title = expTitle;
                    expeditionResult.IsSuccess = false;
                    expeditionResult.Woods = 0;
                    expeditionResult.Stones = 0;
                    expeditionResult.Irons = 0;
                    expeditionResult.Coins = 0;
                    expeditionResult.ExperiencePoints = 0;
                }
            }
            else
            {
                if (winOrLose <= 5) // 50% esély
                {
                    int win = rng.Next(0, 6);
                    message = winText[win];
                    expTitle = "Sikeres expedíció";
                    lootWood = LootCalc(player.Intelligence, rng, difficulty);
                    lootStone = LootCalc(player.Intelligence, rng, difficulty);
                    lootIron = LootCalc(player.Intelligence, rng, difficulty);
                    lootCoin = CoinCalc(player.Intelligence, rng, difficulty);
                    lootExperiencePoints = CoinCalc(player.Intelligence, rng, difficulty);

                    expeditionResult.Message = message;
                    expeditionResult.Title = expTitle;
                    expeditionResult.IsSuccess = true;
                    expeditionResult.Woods = lootWood;
                    expeditionResult.Stones = lootStone;
                    expeditionResult.Irons = lootIron;
                    expeditionResult.Coins = lootCoin;
                    expeditionResult.ExperiencePoints = lootExperiencePoints;
                }
                else
                {
                    int lose = rng.Next(0, 5);
                    message = loseText[lose];
                    expTitle = "Sikertelen expedíció";

                    expeditionResult.Message = message;
                    expeditionResult.Title = expTitle;
                    expeditionResult.IsSuccess = false;
                    expeditionResult.Woods = 0;
                    expeditionResult.Stones = 0;
                    expeditionResult.Irons = 0;
                    expeditionResult.Coins = 0;
                    expeditionResult.ExperiencePoints = 0;
                }
            }


            Notification expeditionNotification = new Notification()
            {
                Player = player,
                Title = expTitle,
                Message = message,
                Wood = lootWood,
                Stone = lootStone,
                Iron = lootIron,
                Coin = lootCoin,
                ExperiencePoint = lootExperiencePoints,
                Date = DateTime.Now
            };

            player.Woods += lootWood;
            player.Stones += lootStone;
            player.Irons += lootIron;
            player.Coins += lootCoin;
            player.ExperiencePoint += lootExperiencePoints;

            await playerRepository.UpdateAsync(player);
            await notificationRepository.AddNotificationAsync(expeditionNotification);

            return expeditionResult;
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
