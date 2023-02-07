using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class NotificationDto
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool IsOpened { get; set; }
    }
}
