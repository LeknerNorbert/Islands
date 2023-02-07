﻿using DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Classified
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
        public Item ReplacementItem { get; set; }
        public int ReplacementAmount { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public PlayerInformation? PlayerInformation { get; set; }
    }
}
