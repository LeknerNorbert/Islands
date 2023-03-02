using DAL.DTOs;
using DAL.Models;
using DAL.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.ExchangeRepository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly ApplicationDbContext _context;

        public ExchangeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Exchange> GetExchangeByIdAsync(int id)
        {
            return await _context.Exchanges
                .Include(c => c.Player)
                    .ThenInclude(p => p.User)
                .FirstAsync(c => c.Id == id);
        }

        public async Task<List<ExchangeDto>> GetAllExchangeAsync()
        {
            return await _context.Exchanges
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

        public async Task<List<ExchangeDto>> GetAllExchangeByUsernameAsync(string username)
        {
            return await _context.Exchanges
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

        public async Task AddExchangeAsync(Exchange exchange)
        {
            await _context.Exchanges.AddAsync(exchange);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveExchangeAsync(Exchange exchange)
        {
            _context.Exchanges.Remove(exchange);
            await _context.SaveChangesAsync();
        }
    }
}
