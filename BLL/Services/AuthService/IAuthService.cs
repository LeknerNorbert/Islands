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
        public void Login(UserLoginRequestDto userLoginRequest, out string token);
        public void Registration(UserRegistrationRequestDto userRegistrationRequest);
        public void VerifyEmail(string token);
        public void ResetPassword(string email);
        public void SetNewPassword(string token, string newPassword);
        public void ResendEmailValidationEmail(string username);
    }
}
