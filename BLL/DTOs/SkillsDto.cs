using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class SkillsDto
    {
        [Required]
        public int Strength { get; set; }
        [Required]
        public int Intelligence { get; set; }
        [Required]
        public int Ability { get; set; }
    }
}
