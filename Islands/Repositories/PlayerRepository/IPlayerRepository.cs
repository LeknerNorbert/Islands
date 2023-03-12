using Islands.Models;
using Islands.Models.DTOs;
using Islands.Models.Enums;

namespace Islands.Repositories.PlayerInformationRepository
{
    public interface IPlayerRepository
    {
        Task<PlayerForBattleDto> GetPlayerForBattleByIdAsync(int id);
        Task<PlayerForBattleDto> GetPlayerForBattleByUsernameAsync(string username);
        Task<Player> GetPlayerByIdAsync(int id);
        Task<IslandType> GetIslandTypeByUsernameAsync(string username);
        Task<Player> GetPlayerByUsernameAsync(string username);
        Task<Player> GetPlayerByForAdAsync(int adId);
        Task AddPlayerAsync(Player player);
        Task UpdatePlayerAsync(Player player);
    }
}
