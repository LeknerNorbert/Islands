using DAL.DTOs;
using DAL.Models.Enums;

namespace BLL.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<PlayerForBattleDto> GetPlayerForBattleByUsernameAsync(string username);
        Task<PlayerDto> GetPlayerByUsernameAsync(string username);
        Task<PlayerDto> AddPlayerAsync(string username, IslandType name);
        Task UpdateSkillsAsync(string username, SkillsDto skills);
        Task UpdateItemsAsync(string username, ItemsDto items);
    }
}
