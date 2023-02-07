using DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Building
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public BuildingType Type { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Level { get; set; }
        public DateTime BuildDate { get; set; }
        public DateTime LastCollectDate { get; set; }
        public PlayerInformation? PlayerInformation { get; set; }
    }
}
