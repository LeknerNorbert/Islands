using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        [Key,MaxLength(20)]
        public string? Username { get; set; }
        [MaxLength(40)]
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? ValidationToken { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ValidateDate { get; set; }
        public DateTime? LastLogout { get; set; }
        public Player? Player { get; set; }
    }
}
