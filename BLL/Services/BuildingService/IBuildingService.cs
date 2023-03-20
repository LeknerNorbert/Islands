using DAL.DTOs;
using DAL.Models.Enums;

namespace BLL.Services.BuildingService
{
    public interface IBuildingService
    {
        Task<BuildingDto> AddBuildingAsync(string username, BuildRequestDto buildingRequest);
        Task<List<BuildingDto>> GetAllBuildingAsync(string username);
        Task<List<UnbuiltBuildingDto>> GetAllUnbuiltBuildingsAsync(string username);
        Task<BuildingDto> UpgradeBuildingAsync(string username, BuildingType building);
        Task<ItemsDto> CollectItemsAsync(string username, BuildingType building);
    }
}
