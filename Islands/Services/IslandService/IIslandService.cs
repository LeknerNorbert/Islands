using Islands.DTOs;

namespace Islands.Services.IslandService
{
    public interface IIslandService
    {
        Task<IslandDto> GetIslandByUsernameAsync(string username);
    }
}
