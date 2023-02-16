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
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAds()
        {
            try
            {
                List<AdDTO> ads = await _adService.GetAllAsync();
                return StatusCode(200, ads);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyAds()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                List<AdDTO> ads = await _adService.GetAllByUsernameAsync(username);

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
        public async Task<IActionResult> CreateAd(NewAdDTO createClassifiedAd)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _adService.AddAsync(username, createClassifiedAd);

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
        public async Task<IActionResult> DeleteAd(int id)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _adService.RemoveAsync(id);

                return StatusCode(200, "Ad successfully deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(200, ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> BuyAd(int id)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _adService.BuyAsync(username, id);

                return StatusCode(200, "Ad successfully buyed.");
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
