using BLL.Exceptions;
using BLL.Services.PlayerService;
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
                string username = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                PlayerDto player = await _playerService.GetPlayerByUsernameAsync(username);

                return Ok(player);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePlayer(IslandType island)
        {
            try
            {
                string username = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                PlayerDto player = await _playerService.AddPlayerAsync(username, island);

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateSkillPoints(SkillsDto skillPoints)
        {
            try
            {
                string username = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                await _playerService.UpdateSkillsAsync(username, skillPoints);

                return Ok("Skill points has been updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> CollectRewards(ItemsDto items)
        {
            try
            {
                string username = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                await _playerService.UpdateItemsAsync(username, items);

                return Ok("Items has been updated!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
