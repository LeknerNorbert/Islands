using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Building
    {
        [Key]
        public int Id { get; set; }
        public Buildings BuildingType { get; set; }
        public int Level { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public Player? Player { get; set; }
    }
}
