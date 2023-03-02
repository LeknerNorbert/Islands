using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
    public class SkillsDto
    {
        [Required]
        public int Strength { get; set; }
        [Required]
        public int Intelligence { get; set; }
        [Required]
        public int Agility { get; set; }
    }
}
