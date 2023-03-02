using DAL.DTOs;

namespace BLL.Services.IslandService
{
    public interface IIslandService
    {
        Task<IslandDto> GetIslandByUsernameAsync(string username);
    }
}
