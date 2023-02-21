using System.ComponentModel.DataAnnotations;

namespace Islands.DTOs
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
