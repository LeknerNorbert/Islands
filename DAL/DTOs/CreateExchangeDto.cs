<<<<<<<< HEAD:DAL/DTOs/CreateExchangeDto.cs
﻿using DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
    public class CreateExchangeDto
========
﻿using Islands.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Islands.DTOs
{
    public class NewAdDto
>>>>>>>> master:Islands/Models/DTOs/CreateClassifiedAdDTO.cs
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
