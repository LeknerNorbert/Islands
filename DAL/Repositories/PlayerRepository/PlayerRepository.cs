using DAL.DTOs;
using DAL.Models;
using DAL.Models.Context;
using DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.PlayerRepository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Player> AddPlayerAsync(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            return player;
        }

        public async Task<IslandType> GetIslandTypeByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(user => user.Player)
                .Where(user => user.Username == username)
                .Select(user => user.Player.SelectedIsland)
                .FirstAsync();
        }

        public async Task<Player> GetPlayerByForAdAsync(int adId)
        {
            Exchange exchange = await _context.Exchanges
                .Include(a => a.Player)
                .FirstAsync(a => a.Id == adId);

            return await _context.Players.FirstAsync(p => p.Id == exchange.Player.Id);
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            return await _context.Players.FirstAsync(p => p.Id == id);
        }

        public async Task<Player> GetPlayerByUsernameAsync(string username)
        {
            return await _context.Players
               .Include(p => p.User)
               .FirstAsync(p => p.User.Username == username);
        }

        public async Task<List<Player>> GetPlayersByExperience(string username, int minExperience, int maxExperience)
        {
            return await _context.Players
                .Include(player => player.User)
                .Where(player => player.Experience >= minExperience && player.Experience <= maxExperience && player.User.Username != username)
                .ToListAsync();
        }

        public async Task UpdatePlayerAsync(Player player)
        {
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Player> GetPlayerWithBuildingsByUsernameAsync(string username)
        {
            return await _context.Players
                .Include(player => player.Buildings)
                .Include(player => player.User)
                .FirstAsync(player => player.User.Username == username);
        }
    }
}
