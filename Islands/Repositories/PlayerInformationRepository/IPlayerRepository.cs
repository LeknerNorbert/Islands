using Islands.Models;

namespace Islands.Repositories.PlayerInformationRepository
{
    public interface IPlayerRepository
    {
        public Player GetPlayerByUsername(string username);
        public void CreatePlayer(Player player);
        public void UpdatePlayerCoins(Player player, int amount);
        public void UpdatePlayerWoods(Player player, int amount);
        public void UpdatePlayerStones(Player player, int amount);
        public void UpdatePlayerIrons(Player player, int amount);
    }
}
