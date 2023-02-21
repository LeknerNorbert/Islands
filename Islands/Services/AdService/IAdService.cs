using Islands.DTOs;

namespace Islands.Services.ClassifiedAdService
{
    public interface IAdService
    {
        Task<List<AdDto>> GetAllAsync();
        Task<List<AdDto>> GetAllByUsernameAsync(string username);
        Task AddAsync(string creatorUsername, NewAdDto createClassifiedAd);
        Task RemoveAsync(int id);
        Task BuyAsync(string customerUsername, int id);
    }
}
