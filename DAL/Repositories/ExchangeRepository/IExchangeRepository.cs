using DAL.DTOs;
using DAL.Models;
using System;
namespace DAL.Repositories.ExchangeRepository
{
    public interface IExchangeRepository
    {
        Task<Exchange> GetExchangeByIdAsync(int id);
        Task<List<ExchangeDto>> GetAllExchangesAsync(string username);
        Task<List<ExchangeDto>> GetAllExchangesByUsernameAsync(string username);
        Task AddExchangeAsync(Exchange exchange);
        Task RemoveExchangeAsync(Exchange exchange);
    }
}
