using BLL.Exceptions;
using BLL.Services.ExchangeService;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;

        public ExchangeController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllExchange()
        {
            try
            {
                List<ExchangeDto> exchanges = await _exchangeService.GetAllExchangeAsync();
                return Ok(exchanges);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllMyExchange()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                List<ExchangeDto> exchanges = await _exchangeService.GetAllExchangeByUsernameAsync(username);

                return Ok(exchanges);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> CreateExchange(CreateExchangeDto exchange)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _exchangeService.AddExchangeAsync(username, exchange);

                return Ok("Exchange successfully created.");
            }
            catch (NotEnoughItemsException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteExchange(int id)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _exchangeService.RemoveExchangeAsync(id);

                return Ok("Exchange successfully deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> BuyExchange(int id)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _exchangeService.BuyExchangeAsync(username, id);

                return StatusCode(200, "Exchange successfully buyed.");
            }
            catch (NotEnoughItemsException ex)
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
