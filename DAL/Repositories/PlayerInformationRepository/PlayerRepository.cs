using DAL.Models;
using DAL.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.PlayerInformationRepository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Player GetPlayerByUsername(string username) 
        {
            return _context.Players
                .Include(p => p.User)
                .First(p => p.User.Username == username);
        }

        public void CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public void UpdatePlayerCoins(Player player, int amount)
        {
            player.Coins += amount;
            _context.SaveChanges();
        }

        public void UpdatePlayerIrons(Player player, int amount)
        {
            player.Irons += amount;
            _context.SaveChanges();
        }

        public void UpdatePlayerStones(Player player, int amount)
        {
            player.Stones += amount;
            _context.SaveChanges();
        }

        public void UpdatePlayerWoods(Player player, int amount)
        {
            player.Woods += amount;
            _context.SaveChanges();
        }
    }
}
