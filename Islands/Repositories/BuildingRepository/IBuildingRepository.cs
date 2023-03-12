using Islands.Models;
using Islands.Models.Enums;

namespace Islands.Repositories.BuildingRepository
{
    public interface IBuildingRepository
    {
        Task AddBuildingAsync(Building building);
        Task<List<Building>> GetAllBuildingAsync(string username);
        Task UpdateBuilding(Building building);
        Task<bool> CheckBuildingIsExist(string username, BuildingType building);
    }
}
