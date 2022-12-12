using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }        
        public string? Subject { get; set; }        
        public string? Body { get; set; }
        public DateTime? SendDate { get; set; }
        public bool IsSeen { get; set; }
        public Player? Player { get; set; }
    }
}
