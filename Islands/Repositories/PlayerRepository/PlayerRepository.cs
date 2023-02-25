using Islands.Exceptions;
using Islands.Models;
using Islands.Models.Context;
using Islands.Models.DTOs;
using Islands.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Islands.Repositories.PlayerInformationRepository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext context;

        public PlayerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddPlayerAsync(Player player)
        {
            try
            {
                await context.Players.AddAsync(player);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding player", ex);
            }
        }

        public async Task<Player> GetPlayerByUsernameAsync(string username) 
        {
             return await context.Players
                .Include(p => p.User)
                .FirstAsync(p => p.User.Username == username);
        }

        public async Task<Player> GetPlayerByForAdAsync(int adId)
        {
            try
            {
                Exchange exchange = await context.Exchanges
                    .Include(a => a.Player)
                    .FirstAsync(a => a.Id == adId);

                return await context.Players.FirstAsync(p => p.Id == exchange.Player.Id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting player", ex);
            }
        }

        public async Task<IslandType> GetIslandTypeByUsernameAsync(string username)
        {
            return await context.Users
                .Include(user => user.Player)
                .Where(user => user.Username == username)
                .Select(user => user.Player.SelectedIsland)
                .FirstAsync();
        }

        public async Task<PlayerForBattleDto> GetPlayerForBattleByUsernameAsync(string username)
        {
            Player player = await context.Players
                .Include(player => player.User)
                .FirstAsync(player => player.User.Username == username);

            Building? church = await context.Buildings
                .Include(building => building.Player)
                .FirstOrDefaultAsync(building => building.Player.Id == player.Id && building.Type == BuildingType.Church);

            Building? practiceRange = await context.Buildings
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

        public async Task<PlayerForBattleDto> GetPlayerForBattleByIdAsync(int id)
        {
            Player player = await context.Players
                .Include(p => p.User)
                .FirstAsync(p => p.Id == id);

            Building? church = await context.Buildings
                .Include(building => building.Player)
                .FirstOrDefaultAsync(building => building.Player.Id == player.Id && building.Type == BuildingType.Church);

            Building? practiceRange = await context.Buildings
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

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            return await context.Players.FirstAsync(p => p.Id == id);
        }

        public async Task UpdatePlayerAsync(Player player)
        {
            try
            {
                context.Entry(player).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating player", ex);
            }
        }
    }
}
