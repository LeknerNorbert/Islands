using DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.DTOs
{
    public class BuildRequestDto
    {
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BuildingType BuildingType { get; set; }
        [Range(0, 25, ErrorMessage="Invalid coordinates")]
        public int XCoordinate { get; set; }
        [Range(0, 15, ErrorMessage = "Invalid coordinates")]
        public int YCoordinate { get; set; }
    }
}
