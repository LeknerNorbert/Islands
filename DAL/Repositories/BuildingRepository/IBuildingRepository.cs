using DAL.DTOs;
using DAL.Models;
using DAL.Models.Enums;

namespace DAL.Repositories.BuildingRepository
{
    public interface IBuildingRepository
    {
        Task AddBuildingAsync(Building building);
        Task<List<Building>> GetAllBuildingByUsernameAsync(string username);
        Task<Building> GetBuildingAsync(string username, BuildingType name);
        Task<List<CoordinateDto>> GetReservedCoordinates(string username);
        Task UpdateBuildingAsync(Building building);
        Task<bool> CheckBuildingIsExist(string username, BuildingType building);
    }
}
