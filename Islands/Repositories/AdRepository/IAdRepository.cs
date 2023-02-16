using Islands.DTOs;
using Islands.Models;

namespace Islands.Repositories.ClassifiedAdRepository
{
    public interface IAdRepository
    {
        Task<Ad> GetByIdAsync(int id);
        Task<List<AdDTO>> GetAllAsync();
        Task<List<AdDTO>> GetAllByUsernameAsync(string username);
        Task AddAsync(Ad classifiedAd);
        Task RemoveAsync(Ad classifiedAd);
    }
}
