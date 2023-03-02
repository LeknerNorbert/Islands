using DAL.DTOs;
using DAL.Models;
using DAL.Models.Enums;

namespace DAL.Repositories.PlayerRepository
{
    public interface IPlayerRepository
    {
        Task<PlayerForBattleDto> GetPlayerForBattleByIdAsync(int id);
        Task<PlayerForBattleDto> GetPlayerForBattleByUsernameAsync(string username);
        Task<Player> GetPlayerByIdAsync(int id);
        Task<IslandType> GetIslandTypeByUsernameAsync(string username);
        Task<Player> GetPlayerByUsernameAsync(string username);
        Task<List<Player>> GetTopSixPlayerByExperience(string username, int minExperience, int maxExperience);
        Task<Player> GetPlayerByForAdAsync(int adId);
        Task AddPlayerAsync(Player player);
        Task UpdatePlayerAsync(Player player);
    }
}
