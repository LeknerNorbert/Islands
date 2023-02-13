using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AuthService
{
    public interface IAuthService
    {
        public void Login(LoginRequestDto userLoginRequest, out string token);
        public void Registration(RegistrationRequestDto userRegistrationRequest);
        public void VerifyEmail(string token);
        public void ResendEmailValidationEmail(string username);
        public void GenerateTemporaryPassword(string email);
        public void ResetPassword(string username, string password);
    }
}
