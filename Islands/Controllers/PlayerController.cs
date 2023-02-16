using Islands.DTOs;
using Islands.Models.Enums;
using Islands.Services.PlayerInformationService;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPlayer()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                PlayerDTO playerInformation = _playerService.GetPlayer(username);

                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("No player created yet.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePlayer([FromBody] IslandType island)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                _playerService.CreatePlayer(username, island);

                return Ok("Player has been created.");
            }
            catch (DbUpdateException)
            {
                return BadRequest("Database error during create.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSkillPoints(SkillsDTO skillPoints)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                _playerService.UpdateSkillPoints(username, skillPoints);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
