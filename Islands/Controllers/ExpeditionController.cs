using Islands.Models.DTOs;
using Islands.Services.BattleService;
using Islands.Services.ExpeditionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Islands.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpeditionController : ControllerBase
    {
        private readonly IExpeditionService expeditionService;

        public ExpeditionController(IExpeditionService expeditionService)
        {
            this.expeditionService = expeditionService;
        }

        [HttpGet]
        public async Task<IActionResult> Expedition()
        {
            try
            {
                ExpeditionResultDto expeditionResult = await expeditionService.GetExpeditionResultAsync(3, 2);
                return StatusCode(200, expeditionResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
