using Islands.DTOs;
using Islands.Exceptions;
using Islands.Models;
using Islands.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Islands.Repositories.ClassifiedAdRepository
{
    public class AdRepository : IAdRepository
    {
        private readonly ApplicationDbContext _context;

        public AdRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Ad> GetByIdAsync(int id)
        {
            return await _context.Ads
                .Include(c => c.PlayerInformation)
                    .ThenInclude(p => p.User)
                .FirstAsync(c => c.Id == id);
        }

        public async Task<List<AdDTO>> GetAllAsync()
        {
            return await _context.Ads
                .Where(c => c.PublishDate >= DateTime.Now.AddDays(-7))
                .OrderBy(c => c.PublishDate)
                .Select(c => new AdDTO()
                {
                    Id = c.Id,
                    Item = c.Item,
                    Amount = c.Amount,
                    ReplacementItem = c.ReplacementItem,
                    ReplacementAmount = c.ReplacementAmount,
                    PublishDate = c.PublishDate
                })
                .ToListAsync();
        }

        public async Task<List<AdDTO>> GetAllByUsernameAsync(string username)
        {
            try
            {
                return await _context.Ads
                    .Include(c => c.PlayerInformation)
                        .ThenInclude(p => p.User)
                    .Where(c => c.PlayerInformation.User.Username == username)
                    .OrderBy(c => c.PublishDate)
                    .Select(c => new AdDTO()
                    {
                        Id = c.Id,
                        Item = c.Item,
                        Amount = c.Amount,
                        ReplacementItem = c.ReplacementItem,
                        ReplacementAmount = c.ReplacementAmount,
                        PublishDate = c.PublishDate
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Could not get classified ads", ex);
            }
        }

        public async Task AddAsync(Ad classifiedAd)
        {
            try
            {
                await _context.Ads.AddAsync(classifiedAd);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding classified ad.", ex);
            }
        }

        public async Task RemoveAsync(Ad classifiedAd)
        {
            try
            {
                _context.Ads.Remove(classifiedAd);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding classified ad.", ex);
            }
        }
    }
}
