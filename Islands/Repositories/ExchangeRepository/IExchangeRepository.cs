using Islands.DTOs;
using Islands.Models;

namespace Islands.Repositories.ClassifiedAdRepository
{
    public interface IExchangeRepository
    {
        Task<Exchange> GetByIdAsync(int id);
        Task<List<ExchangeDto>> GetAllAsync();
        Task<List<ExchangeDto>> GetAllByUsernameAsync(string username);
        Task AddAsync(Exchange classifiedAd);
        Task RemoveAsync(Exchange classifiedAd);
    }
}
