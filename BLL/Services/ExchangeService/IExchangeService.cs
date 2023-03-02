using DAL.DTOs;

namespace BLL.Services.ExchangeService
{
    public interface IExchangeService
    {
        Task<List<ExchangeDto>> GetAllExchangeAsync();
        Task<List<ExchangeDto>> GetAllExchangeByUsernameAsync(string username);
        Task AddExchangeAsync(string username, CreateExchangeDto exchange);
        Task RemoveExchangeAsync(int id);
        Task BuyExchangeAsync(string username, int id);
    }
}
