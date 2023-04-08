using BLL.Exceptions;
using BLL.Services.BuildingService;
using DAL.DTOs;
using DAL.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                string username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
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
        public async Task<IActionResult> GetAllBuildings()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                List<BuildingDto> buildings = await _buildingService.GetAllBuildingsAsync(username);

                return Ok(buildings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUnbuiltBuildings()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                List<UnbuiltBuildingDto> buildings = await _buildingService.GetAllUnbuiltBuildingsAsync(username);

                return Ok(buildings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetNextLevelOfBuilding(BuildingType type)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                UnbuiltBuildingDto nextLevelBuilding = await _buildingService.GetNextLevelOfBuildingByIslandAsync(username, type);

                return Ok(nextLevelBuilding);
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
                string username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
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
        public async Task<IActionResult> CollectItems(CollectRequestDto collectRequest)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                ItemsDto receivedItems = await _buildingService.CollectItemsAsync(username, collectRequest);

                return Ok(receivedItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
