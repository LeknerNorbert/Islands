﻿using System.ComponentModel.DataAnnotations;

namespace Islands.DTOs
{
    public class PasswordResetDTO
    {
        [Required] 
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", 
            ErrorMessage = "Password must be at least eight characters long and contain uppercase letters and numbers.")]
        public string? Password { get; set; }
        [Required, Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
