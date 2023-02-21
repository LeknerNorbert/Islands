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
        [Authorize]
        [HttpGet]
        public IActionResult Battlefield(int enemyId)
        {
            return StatusCode(200);
        }
    }
}
