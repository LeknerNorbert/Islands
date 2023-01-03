using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    // Csak egyedi nevet és emailt lehet megadni, ami még nem szerepel az adatbázisban
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? ValidationToken { get; set; }
        public DateTime ValidationDate { get; set; }
        public string? ResetToken { get; set; }
        public virtual PlayerInformation? PlayerInformation { get; set; }
    }
}
