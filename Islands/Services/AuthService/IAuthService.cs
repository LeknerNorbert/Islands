using Islands.DTOs;

namespace Islands.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequestWithUsernameDto login);
        Task RegistrationAsync(RegistrationRequestDto registration);
        Task<bool> VerifyEmailAsync(string token);
        Task ResendVerifyEmailAsync(string username);
        Task SetTemporaryPasswordAsync(string email);
        Task UpdatePasswordAsync(string username, PasswordResetDto password);
    }
}
