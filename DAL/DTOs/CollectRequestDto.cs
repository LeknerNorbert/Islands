using DAL.Attributes;
using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class CollectRequestDto
    {
        [DateNotGreaterThanToday(ErrorMessage = "Invalid collect date.")]
        public DateTime CollectDate { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BuildingType BuildingType { get; set; }
    }
}
