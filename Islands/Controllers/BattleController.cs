using BLL.Exceptions;
using BLL.Services.BattleService;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService _battleService;
        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> StartBattle(int enemyId)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                BattleReportDto battleResult = await _battleService.GetBattleReportAsync(username, enemyId);
                
                return Ok(battleResult);
            }
            catch (BattleNotAllowedException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllEnemies()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                List<EnemyDto> enemies = await _battleService.GetAllEnemiesAsync(username);

                return Ok(enemies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
