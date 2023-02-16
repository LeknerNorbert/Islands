using Islands.Exceptions;
using Islands.Models;
using Islands.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Islands.Repositories.PlayerInformationRepository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Player player)
        {
            try
            {
                await _context.Players.AddAsync(player);
                await _context.SaveChangesAsync();
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
                return await _context.Players.Include(p => p.User).FirstAsync(p => p.User.Username == username);
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
                Ad ad = await _context.Ads
                    .Include(a => a.PlayerInformation)
                    .FirstAsync(a => a.Id == adId);

                return await _context.Players.FirstAsync(p => p.Id == ad.PlayerInformation.Id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting player", ex);
            }
        }

        public async Task UpdateAsync(Player player)
        {
            try
            {
                _context.Entry(player).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating player", ex);
            }
        }
    }
}
