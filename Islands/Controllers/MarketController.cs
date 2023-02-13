using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.ClassifiedAdService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IClassifiedAdService _classifiedAdService;

        public MarketController(IClassifiedAdService classifiedAdService)
        {
            _classifiedAdService = classifiedAdService;
        }

        [HttpGet, /*Authorize(Roles = "User")*/]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetClassifieds()
        {
            try
            {
                List<ClassifiedAdDto> classifieds = _classifiedAdService.GetClassifiedAds();
                return Ok(classifieds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, /*Authorize(Roles = "User")*/]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetMyClassifieds()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                List<ClassifiedAdDto> classifieds = _classifiedAdService.GetClassifiedAdsByUser(username);

                return Ok(classifieds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost,/*Authorize(Roles = "User")*/]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateClassifiedAd(CreateClassifiedAdDto createClassifiedAd)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                _classifiedAdService.CreateClassifiedAd(username, createClassifiedAd);

                return Ok("Classified ad successfully created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, /*Authorize(Roles = "User")*/]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteClassifiedAd(int id)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                _classifiedAdService.DeleteClassifiedAd(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut, /*Authorize(Roles = "User")*/]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PurchaseClassifiedAd(int id)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                _classifiedAdService.PurchaseClassifiedAd(username, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
