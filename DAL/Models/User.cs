using DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    // Csak egyedi nevet és emailt lehet megadni, ami még nem szerepel az adatbázisban
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? EmailValidationToken { get; set; }
        public DateTime EmailValidationTokenExpiration { get; set; }
        public RoleType Role { get; set; }
        public virtual Player? PlayerInformation { get; set; }
    }
}
