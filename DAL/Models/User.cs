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
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public string EmailValidationToken { get; set; } = string.Empty;
        public DateTime EmailValidationTokenExpiration { get; set; }
        public DateTime EmailValidationDate { get; set; }
        public virtual Player? Player { get; set; }
    }
}
