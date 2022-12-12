using DAL.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public int XP { get; set; }
        public int Coins { get; set; }
        public int Woods { get; set; }
        public int Stones { get; set; }
        public int Irons { get; set; }
        public DateTime? LastExpeditionDate { get; set; }
        public DateTime? LastBattleDate { get; set; }
        public int Points { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Ability { get; set; }
        public IslandTypes IslandType { get; set; }
        public User? User { get; set; }
        public List<Advertisement>? Advertisements { get; set; }
        public List<Building>? Buildings { get; set; }
        public List<Notification>? Notifications { get; set; }
    }

}
