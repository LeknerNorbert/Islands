using Islands.DTOs;
using Islands.Models.DTOs;
using Islands.Services.BuildingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService buildingService;

        public BuildingController(IBuildingService buildingServic)
        {
            this.buildingService = buildingServic;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBuiling(BuildingRequestDto building)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                BuildingDto createdBuilding = await buildingService.AddBuildingAsync(username, building);

                return StatusCode(201, createdBuilding);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBuildings()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpgradeBuilding([FromBody] int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CollectItems([FromBody] int id)
        {
            return Ok();
        }
    }
}