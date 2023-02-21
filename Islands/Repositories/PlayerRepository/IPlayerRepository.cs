using Islands.Models;
using Islands.Models.DTOs;

namespace Islands.Repositories.PlayerInformationRepository
{
    public interface IPlayerRepository
    {
        Task<PlayerForBattleDto?> GetPlayerForBattleByIdAsync(int id);
        Task<Player?> GetPlayerByIdAsync(int id);
        Task<Player> GetByUsernameAsync(string username);
        Task<Player> GetByForAd(int adId);
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);
    }
}
