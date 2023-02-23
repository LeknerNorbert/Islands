using Islands.DTOs;
using Islands.Exceptions;
using Islands.Models.Enums;
using Islands.Services.IslandService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IslandController : ControllerBase
    {
        private readonly IIslandService islandService;

        public IslandController(IIslandService islandService)
        {
            this.islandService = islandService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetIsland()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;

                IslandDto island = await islandService.GetIslandByUsernameAsync(username);
                return StatusCode(200, island);
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode(500, ex.ToString());
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }
    }
}
