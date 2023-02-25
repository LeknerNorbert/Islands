using Islands.DTOs;

namespace Islands.Services.ClassifiedAdService
{
    public interface IExchangeService
    {
        Task<List<ExchangeDto>> GetAllAsync();
        Task<List<ExchangeDto>> GetAllByUsernameAsync(string username);
        Task AddAsync(string creatorUsername, NewAdDto createClassifiedAd);
        Task RemoveAsync(int id);
        Task BuyAsync(string customerUsername, int id);
    }
}
