using Islands.DTOs;

namespace Islands.Services.ClassifiedAdService
{
    public interface IAdService
    {
        Task<List<AdDTO>> GetAllAsync();
        Task<List<AdDTO>> GetAllByUsernameAsync(string username);
        Task AddAsync(string creatorUsername, NewAdDTO createClassifiedAd);
        Task RemoveAsync(int id);
        Task BuyAsync(string customerUsername, int id);
    }
}
