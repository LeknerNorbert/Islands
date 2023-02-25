using Islands.Models.DTOs;
using Islands.Services.BattleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService battleService;
        public BattleController(IBattleService battleService)
        {
            this.battleService = battleService;
        }

        [HttpGet]
        public async Task<IActionResult> Battle(int enemyId)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;

                BattleResultDto battleResult = await battleService.GetBattleResultAsync(username, enemyId);
                return StatusCode(200, battleResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
