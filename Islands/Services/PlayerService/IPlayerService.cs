using Islands.DTOs;
using Islands.Models.Enums;

namespace Islands.Services.PlayerInformationService
{
    public interface IPlayerService
    {
        Task<PlayerDto> GetByUsernameAsync(string username);
        Task AddAsync(string username, IslandType island);
        Task UpdateSkillsAsync(string username, SkillsDto skills);
    }
}
