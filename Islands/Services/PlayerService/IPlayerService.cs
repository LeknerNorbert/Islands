using Islands.DTOs;
using Islands.Models.Enums;

namespace Islands.Services.PlayerInformationService
{
    public interface IPlayerService
    {
        Task<PlayerDTO> GetByUsernameAsync(string username);
        Task AddAsync(string username, IslandType island);
        Task UpdateSkillsAsync(string username, SkillsDTO skills);
    }
}
