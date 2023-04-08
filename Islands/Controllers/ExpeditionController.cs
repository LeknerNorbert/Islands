using BLL.Exceptions;
using BLL.Services.ExpeditionService;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpeditionController : ControllerBase
    {
        private readonly IExpeditionService _expeditionService;

        public ExpeditionController(IExpeditionService expeditionService)
        {
            _expeditionService = expeditionService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Expedition(int difficulty)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                ExpeditionReportDto expeditionResult = await _expeditionService.GetExpeditionReportAsync(username, difficulty);
                
                return Ok(expeditionResult);
            }
            catch (ExpeditionNotAllowedException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
