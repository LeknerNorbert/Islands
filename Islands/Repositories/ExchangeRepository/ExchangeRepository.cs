using Islands.DTOs;
using Islands.Exceptions;
using Islands.Models;
using Islands.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Islands.Repositories.ClassifiedAdRepository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly ApplicationDbContext context;

        public ExchangeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Exchange> GetByIdAsync(int id)
        {
            return await context.Exchanges
                .Include(c => c.Player)
                    .ThenInclude(p => p.User)
                .FirstAsync(c => c.Id == id);
        }

        public async Task<List<ExchangeDto>> GetAllAsync()
        {
            return await context.Exchanges
                .Where(c => c.PublishDate >= DateTime.Now.AddDays(-7))
                .OrderBy(c => c.PublishDate)
                .Select(c => new ExchangeDto()
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

        public async Task<List<ExchangeDto>> GetAllByUsernameAsync(string username)
        {
            try
            {
                return await context.Exchanges
                    .Include(c => c.Player)
                        .ThenInclude(p => p.User)
                    .Where(c => c.Player.User.Username == username)
                    .OrderBy(c => c.PublishDate)
                    .Select(c => new ExchangeDto()
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

        public async Task AddAsync(Exchange classifiedAd)
        {
            try
            {
                await context.Exchanges.AddAsync(classifiedAd);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding classified ad.", ex);
            }
        }

        public async Task RemoveAsync(Exchange classifiedAd)
        {
            try
            {
                context.Exchanges.Remove(classifiedAd);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding classified ad.", ex);
            }
        }
    }
}
