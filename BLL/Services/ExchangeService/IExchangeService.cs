using DAL.DTOs;

namespace BLL.Services.ExchangeService
{
    public interface IExchangeService
    {
        Task<List<ExchangeDto>> GetAllExchangesAsync(string username);
        Task<List<ExchangeDto>> GetAllExchangesByUsernameAsync(string username);
        Task AddExchangeAsync(string username, CreateExchangeDto exchange);
        Task RemoveExchangeAsync(string username, int id);
        Task BuyExchangeAsync(string username, int id);
    }
}
