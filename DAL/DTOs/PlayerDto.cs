<<<<<<<< HEAD:DAL/DTOs/PlayerDto.cs
﻿using DAL.Models.Enums;

namespace DAL.DTOs
========
﻿using Islands.Models.Enums;

namespace Islands.DTOs
>>>>>>>> master:Islands/Models/DTOs/PlayerDTO.cs
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public int Experience { get; set; }
        public int Coins { get; set; }
        public int Woods { get; set; }
        public int Stones { get; set; }
        public int Irons { get; set; }
<<<<<<<< HEAD:DAL/DTOs/PlayerDto.cs
        public string SelectedIsland { get; set; } = string.Empty;
========
        public IslandType SelectedIsland { get; set; }
>>>>>>>> master:Islands/Models/DTOs/PlayerDTO.cs
        public DateTime LastExpeditionDate { get; set; }
        public DateTime LastBattleDate { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
    }
}
