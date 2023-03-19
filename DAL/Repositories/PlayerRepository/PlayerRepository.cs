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

        public async Task<PlayerForBattleDto> GetPlayerForBattleByIdAsync(int id)
        {
            Player player = await _context.Players
                .Include(p => p.User)
                .FirstAsync(p => p.Id == id);

            Building? church = await _context.Buildings
                .Include(building => building.Player)
                .FirstOrDefaultAsync(building => building.Player.Id == player.Id && building.Type == BuildingType.Church);

            Building? practiceRange = await _context.Buildings
                .Include(building => building.Player)
                .FirstOrDefaultAsync(building => building.Player.Id == player.Id && building.Type == BuildingType.PracticeRange);

            PlayerForBattleDto playerForBattle = new()
            {
                Id = player.Id,
                Username = player.User.Username,
                Intelligence = player.Intelligence,
                Strength = player.Strength,
                Agility = player.Agility,
                Health = 170,
                ChurchLevel = church != null ? church.Level : 0,
                PracticeRangeLevel = practiceRange != null ? practiceRange.Level : 0,
                LastBattleDate = player.LastBattleDate
            };

            return playerForBattle;
        }

        public async Task<PlayerForBattleDto> GetPlayerForBattleByUsernameAsync(string username)
        {
            Player player = await _context.Players
                .Include(player => player.User)
                .FirstAsync(player => player.User.Username == username);

            Building? church = await _context.Buildings
                .Include(building => building.Player)
                .FirstOrDefaultAsync(building => building.Player.Id == player.Id && building.Type == BuildingType.Church);

            Building? practiceRange = await _context.Buildings
                .Include(building => building.Player)
                .FirstOrDefaultAsync(building => building.Player.Id == player.Id && building.Type == BuildingType.PracticeRange);

            PlayerForBattleDto playerForBattle = new()
            {
                Id = player.Id,
                Username = player.User.Username,
                Intelligence = player.Intelligence,
                Strength = player.Strength,
                Agility = player.Agility,
                Health = 170,
                ChurchLevel = church != null ? church.Level : 0,
                PracticeRangeLevel = practiceRange != null ? practiceRange.Level : 0,
                LastBattleDate = player.LastBattleDate
            };

            return playerForBattle;
        }

        public async Task<List<Player>> GetTopSixPlayerByExperience(string username, int minExperience, int maxExperience)
        {
            return await _context.Players
                .Include(player => player.User)
                .Where(player => player.Experience >= minExperience && player.Experience <= maxExperience && player.User.Username != username)
                .Take(6)
                .ToListAsync();
        }

        public async Task UpdatePlayerAsync(Player player)
        {
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
