using DAL.DTOs;

namespace BLL.Services.IslandService
{
    public interface IIslandService
    {
        Task<IslandDto> GetIslandByUsernameAsync(string username);
        Task<SkillsDto> GetDefaultSkillsByUsernameAsync(string username);
        Task<SkillsDto> GetMaximumSkillPointsAsync();
    }
}
