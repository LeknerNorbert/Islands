using Islands.DTOs;

namespace Islands.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> Login(LoginRequestWithUsernameDTO login);
        Task Registration(RegistrationRequestDTO registration);
        Task<bool> VerifyEmail(string token);
        Task ResendEmailVerificationEmail(string username);
        Task GenerateTemporaryPassword(string email);
        Task ResetPassword(string username, string password);
    }
}
