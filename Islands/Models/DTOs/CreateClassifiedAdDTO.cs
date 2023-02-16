using Islands.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Islands.DTOs
{
    public class NewAdDTO
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
