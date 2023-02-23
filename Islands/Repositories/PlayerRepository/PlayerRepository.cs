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

        public async Task AddAsync(Player player)
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

        public async Task<Player> GetByUsernameAsync(string username) 
        {
            try
            {
                return await context.Players.Include(p => p.User).FirstAsync(p => p.User.Username == username);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting player", ex);
            }
        }

        public async Task<Player> GetByForAd(int adId)
        {
            try
            {
                Ad ad = await context.Ads
                    .Include(a => a.PlayerInformation)
                    .FirstAsync(a => a.Id == adId);

                return await context.Players.FirstAsync(p => p.Id == ad.PlayerInformation.Id);
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

        public async Task<PlayerForBattleDto?> GetPlayerForBattleByIdAsync(int id)
        {
            PlayerForBattleDto playerForBattle = new();

            Player? player = await context.Players
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            Building? church = await context.Buildings
                .Include(b => b.PlayerInformation)
                .FirstOrDefaultAsync(b => b.PlayerInformation.Id == id && b.Type == BuildingType.Church);

            Building? practiceRange = await context.Buildings
                .Include(b => b.PlayerInformation)
                .FirstOrDefaultAsync(b => b.PlayerInformation.Id == id && b.Type == BuildingType.PracticeRange);

            if (church != null)
            {
                playerForBattle.ChurchLevel = church.Level;
            }
            else
            {
                playerForBattle.ChurchLevel = 0;
            }

            if (practiceRange != null)
            {
                playerForBattle.PracticeRangeLevel = practiceRange.Level;
            }
            else
            {
                playerForBattle.PracticeRangeLevel = 0;
            }

            if (player != null)
            {
                playerForBattle.Id = player.Id;
                playerForBattle.Username = player.User.Username;
                playerForBattle.Intelligence = player.Intelligence;
                playerForBattle.Strength = player.Strength;
                playerForBattle.Agility = player.Agility;
                playerForBattle.Health = 170;
            }
            else
            {
                return null;
            }

            return playerForBattle; 
        }

        public async Task<Player?> GetPlayerByIdAsync(int id)
        {
            return await context.Players.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Player player)
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
