using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CreateClassifiedAdDto
    {
        [Required]
        public Item Item { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public Item ReplacementItem { get; set; }
        [Required]
        public int ReplacementAmount { get; set; }
    }
}
