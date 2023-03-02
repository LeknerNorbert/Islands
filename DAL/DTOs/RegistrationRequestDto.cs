using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
    public class RegistrationRequestDto
    {
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", 
            ErrorMessage = "Password must be at least eight characters long and contain uppercase letters and numbers.")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
