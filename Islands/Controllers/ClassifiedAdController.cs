using BLL.DTOs;
using BLL.Services.ClassifiedAdService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifiedAdController : ControllerBase
    {
        private readonly IClassifiedAdService _classifiedAdService;

        public ClassifiedAdController(IClassifiedAdService classifiedAdService)
        {
            _classifiedAdService = classifiedAdService;
        }

        [HttpGet, Authorize(Roles = "User")]
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

        [HttpGet, Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetMyClassifieds()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                List<ClassifiedAdDto> classifieds = _classifiedAdService.GetMyClassifiedAds(username);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateClassifiedAd(CreateClassifiedAdDto createClassifiedAd)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                _classifiedAdService.CreateClassifiedAd(username, createClassifiedAd);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "User")]
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
    }
}
