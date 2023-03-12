using BLL.Services.NotificationService;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllNotification()
        {
            try
            {
                string username = User.Claims.First(claim => claim.Type == "Username").Value;
                List<NotificationDto> notifications = await _notificationService.GetAllNotificationByUsernameAsync(username);

                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                await _notificationService.RemoveNotificationAsync(id);

                return Ok("Notification successfully removed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
