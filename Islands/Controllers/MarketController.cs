using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.ClassifiedAdService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

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

        [HttpGet]
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

        [HttpGet]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateModel]
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
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


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
