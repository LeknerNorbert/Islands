<<<<<<<< HEAD:DAL/DTOs/ExchangeDto.cs
﻿using DAL.Models.Enums;

namespace DAL.DTOs
========
﻿using Islands.Models.Enums;

namespace Islands.DTOs
>>>>>>>> master:Islands/Models/DTOs/ExchangeDto.cs
{
    public class ExchangeDto
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
        public Item ReplacementItem { get; set; }
        public int ReplacementAmount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
