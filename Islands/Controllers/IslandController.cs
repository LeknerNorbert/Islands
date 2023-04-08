using BLL.Services.IslandService;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                string username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                IslandDto island = await _islandService.GetIslandByUsernameAsync(username);

                return Ok(island);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDefaultSkills()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                SkillsDto defaultSkills = await _islandService.GetDefaultSkillsByUsernameAsync(username);

                return Ok(defaultSkills);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMaximumSkillPoints()
        {
            try
            {
                SkillsDto maximumSkillPoints = await _islandService.GetMaximumSkillPointsAsync();

                return Ok(maximumSkillPoints);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
