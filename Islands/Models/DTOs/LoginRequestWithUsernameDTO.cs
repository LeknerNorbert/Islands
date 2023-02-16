using System.ComponentModel.DataAnnotations;

namespace Islands.DTOs
{
    public class LoginRequestWithUsernameDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
