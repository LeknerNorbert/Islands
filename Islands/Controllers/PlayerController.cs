using BLL.DTOs;
using BLL.Services.PlayerInformationService;
using DAL.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerInformationService;

        public PlayerController(IPlayerService playerInformationService)
        {
            _playerInformationService = playerInformationService;
        }

        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPlayer()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                PlayerDto playerInformation = _playerInformationService.GetPlayer(username);

                return Ok(playerInformation);
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

        [HttpPost, Authorize]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePlayer([FromBody] IslandType island)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                _playerInformationService.CreatePlayer(username, island);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut, Authorize]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSkillPoints(SkillsDto skillPoints)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
