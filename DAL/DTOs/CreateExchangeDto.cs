﻿using DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.DTOs
{
    public class CreateExchangeDto
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
