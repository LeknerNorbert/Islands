using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }
        public Items Item { get; set; }
        public int Amount { get; set; }
        public Items ReplacemantItem { get; set; }
        public int ReplacemantAmount { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsPurchased { get; set; }
        public Player? Player { get; set; }
    }
}
