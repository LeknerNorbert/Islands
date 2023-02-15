using Islands.DTOs;

namespace Islands.Services.AuthService
{
    public interface IAuthService
    {
        public void Login(LoginRequestDTO userLoginRequest, out string token);
        public void Registration(RegistrationRequestDTO userRegistrationRequest);
        public void VerifyEmail(string token);
        public void ResendEmailValidationEmail(string username);
        public void GenerateTemporaryPassword(string email);
        public void ResetPassword(string username, string password);
    }
}
