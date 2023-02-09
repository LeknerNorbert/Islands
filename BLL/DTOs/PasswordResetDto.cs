using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class PasswordResetDto
    {
        [Required, RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Password must be at least eight characters long and contain uppercase letters and numbers.")]
        public string? Password { get; set; }
        [Required, Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
