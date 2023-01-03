using DAL.Models;
using DAL.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Notification>  GetNotifications()
        {
            List<Notification> notifications = _context.Notifications.ToList();

            return notifications;
        }
    }
}
