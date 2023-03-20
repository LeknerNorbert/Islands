using BLL.Services.BuildingService;
using DAL.DTOs;
using DAL.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingServic)
        {
            _buildingService = buildingServic;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBuilding(BuildRequestDto building)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                BuildingDto addedBuilding = await _buildingService.AddBuildingAsync(username, building);

                return Ok(addedBuilding);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBuilding()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                List<BuildingDto> buildings = await _buildingService.GetAllBuildingAsync(username);

                return Ok(buildings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUnbuiltBuilding()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                List<UnbuiltBuildingDto> buildings = await _buildingService.GetAllUnbuiltBuildingsAsync(username);

                return Ok(buildings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpgradeBuilding(BuildingType type)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                BuildingDto building = await _buildingService.UpgradeBuildingAsync(username, type);

                return Ok(building);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CollectItems(BuildingType type)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                ItemsDto receivedItems = await _buildingService.CollectItemsAsync(username, type);

                return Ok(receivedItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
