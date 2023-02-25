using Islands.DTOs;
using Islands.Exceptions;
using Islands.Filters;
using Islands.Services.ClassifiedAdService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeService exchangeService;

        public ExchangeController(IExchangeService exchangeService)
        {
            this.exchangeService = exchangeService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetExchanges()
        {
            try
            {
                List<ExchangeDto> ads = await exchangeService.GetAllAsync();
                return StatusCode(200, ads);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyExchanges()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                List<ExchangeDto> ads = await exchangeService.GetAllByUsernameAsync(username);

                return StatusCode(200, ads);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> CreateExchange(NewAdDto createClassifiedAd)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await exchangeService.AddAsync(username, createClassifiedAd);

                return Ok("Classified ad successfully created.");
            }
            catch (InsufficientItemsException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteExchange(int id)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await exchangeService.RemoveAsync(id);

                return StatusCode(200, "Exchange successfully deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(200, ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> BuyExchange(int id)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await exchangeService.BuyAsync(username, id);

                return StatusCode(200, "Exchange successfully buyed.");
            }
            catch (InsufficientItemsException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
