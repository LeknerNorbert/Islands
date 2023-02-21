using Islands.DTOs;
using Islands.Models.Enums;
using Islands.Services.PlayerInformationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerInformationService)
        {
            _playerService = playerInformationService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPlayer()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                PlayerDto player = await _playerService.GetByUsernameAsync(username);

                return StatusCode(200, player);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePlayer([FromBody] IslandType island)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _playerService.AddAsync(username, island);

                return StatusCode(201, "Player has been created.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateSkillPoints(SkillsDto skillPoints)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _playerService.UpdateSkillsAsync(username, skillPoints);

                return StatusCode(200, "Skill points has been updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
