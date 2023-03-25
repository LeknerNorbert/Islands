using DAL.DTOs;
using DAL.Models.Enums;

namespace BLL.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<PlayerDto> GetPlayerByUsernameAsync(string username);
        Task<PlayerDto> AddPlayerAsync(string username, IslandType name);
        Task UpdateSkillsAsync(string username, SkillsDto skills);
        Task UpdateItemsAsync(string username, ItemsDto items);
    }
}
