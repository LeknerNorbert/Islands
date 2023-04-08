using BLL.Services.NotificationService;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> GetAllNotifications()
        {
            try
            {
                string username = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                List<NotificationDto> notifications = await _notificationService.GetAllNotificationsByUsernameAsync(username);

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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SetNotificationToOpened(int id)
        {
            try
            {
                await _notificationService.SetNotificationToOpenedAsync(id);
                return Ok("Notification successfully modified.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
