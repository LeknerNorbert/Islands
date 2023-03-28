using DAL.DTOs;

namespace BLL.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequestDto login);
        Task RegistrationAsync(RegistrationRequestDto registration);
        Task<bool> VerifyEmailAsync(string token);
        Task ResendVerifyEmailAsync(string username);
        Task SetTemporaryPasswordAsync(string email);
        Task UpdatePasswordAsync(string username, PasswordChangeDto password);
        Task<DateTime> GetEmailValidationDateByUsernameAsync(string username);
    }
}
