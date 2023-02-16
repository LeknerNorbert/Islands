using Islands.Models;

namespace Islands.Repositories.PlayerInformationRepository
{
    public interface IPlayerRepository
    {
        Task<Player> GetByUsernameAsync(string username);
        Task<Player> GetByForAd(int adId);
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);
    }
}
