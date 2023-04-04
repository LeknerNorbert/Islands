using DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.DTOs
{
    public class CreateExchangeDto
    {
        [Required]
        public Item Item { get; set; }
        [Required]
        [Range(1, 10000)]
        public int Amount { get; set; }
        [Required]
        public Item ReplacementItem { get; set; }
        [Required]
        [Range(1, 10000)]
        public int ReplacementAmount { get; set; }
    }
}
