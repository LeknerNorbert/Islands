using BLL.Services.IslandService;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IslandController : ControllerBase
    {
        private readonly IIslandService _islandService;

        public IslandController(IIslandService islandService)
        {
            _islandService = islandService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetIsland()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                IslandDto island = await _islandService.GetIslandByUsernameAsync(username);

                return Ok(island);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
