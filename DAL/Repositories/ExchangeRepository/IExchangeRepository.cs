using DAL.DTOs;
using DAL.Models;
using System;
namespace DAL.Repositories.ExchangeRepository
{
    public interface IExchangeRepository
    {
        Task<Exchange> GetExchangeByIdAsync(int id);
        Task<List<ExchangeDto>> GetAllExchangeAsync();
        Task<List<ExchangeDto>> GetAllExchangeByUsernameAsync(string username);
        Task AddExchangeAsync(Exchange exchange);
        Task RemoveExchangeAsync(Exchange exchange);
    }
}
