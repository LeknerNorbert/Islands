using DAL.DTOs;
using DAL.Models.Enums;

namespace BLL.Services.BuildingService
{
    public interface IBuildingService
    {
        Task<BuildingDto> AddBuildingAsync(string username, BuildRequestDto buildingRequest);
        Task<List<BuildingDto>> GetAllBuildingsAsync(string username);
        Task<List<UnbuiltBuildingDto>> GetAllUnbuiltBuildingsAsync(string username);
        Task<UnbuiltBuildingDto> GetNextLevelOfBuildingByIslandAsync(string username, BuildingType buildingType);
        Task<BuildingDto> UpgradeBuildingAsync(string username, BuildingType buildingType);
        Task<ItemsDto> CollectItemsAsync(string username, BuildingType buildingType);
    }
}
