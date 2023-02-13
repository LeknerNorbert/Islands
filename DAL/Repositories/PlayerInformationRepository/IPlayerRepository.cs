using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.PlayerInformationRepository
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
