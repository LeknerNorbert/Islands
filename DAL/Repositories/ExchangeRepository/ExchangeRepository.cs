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

        public async Task<List<ExchangeDto>> GetAllExchangesAsync(string username)
        {
            return await _context.Exchanges
                .Include(exchange => exchange.Player)
                    .ThenInclude(player => player.User)
                .Where(exchange => exchange.PublishDate >= DateTime.Now.AddDays(-7) && exchange.Player.User.Username != username)
                .OrderByDescending(exchange => exchange.PublishDate)
                .Select(exchange => new ExchangeDto()
                {
                    Id = exchange.Id,
                    Item = exchange.Item,
                    Amount = exchange.Amount,
                    ReplacementItem = exchange.ReplacementItem,
                    ReplacementAmount = exchange.ReplacementAmount,
                    PublishDate = exchange.PublishDate
                })
                .ToListAsync();
        }

        public async Task<List<ExchangeDto>> GetAllExchangesByUsernameAsync(string username)
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
