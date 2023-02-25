using Islands.DTOs;
using Islands.Models.DTOs;
using Islands.Models.Enums;

namespace Islands.Services.BuildingService
{
    public interface IBuildingService
    {
        Task<BuildingDto> AddBuildingAsync(string username, BuildingRequestDto buildingRequest);
        Task<List<BuildingDto>> GetAllBuildingAsync(string username);
        Task<BuildingDto> UpgradeBuildingAsync(string username, BuildingType building);
        Task CollectItemsAsync(string username, BuildingType building);
    }
}
