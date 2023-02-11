using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.EmailService;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.UserRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(IUserRepository userRepository,  IConfiguration configuration, IEmailService emailService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _emailService = emailService;
        }

        public void Login(UserLoginRequestDto userLoginRequest, out string token)
        {
            User user = _userRepository.GetUserByUsername(userLoginRequest.Username);
     
            if (VerifyPasswordHash(userLoginRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                token = CreateToken(user);
            }
            else
            {
                throw new LoginValidationException("Credentials are incorrect.");
            }
        }

        public void Registration(UserRegistrationRequestDto userRegistrationRequest)
        {
            CreatePasswordHash(userRegistrationRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);
            
            string validationToken = CreateRandomToken();

            User user = new()
            {
                Username = userRegistrationRequest.Username,
                Email = userRegistrationRequest.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = RoleType.Guest,
                EmailValidationToken = validationToken,
                EmailValidationTokenExpiration = DateTime.Now.AddMinutes(10)
            };

            _userRepository.CreateUser(user);
            SendEmailValidationEmail(user.Email, validationToken);
        }

        public void ResendEmailValidationEmail(string username)
        {
            User user = _userRepository.GetUserByUsername(username);
            string validationToken = CreateRandomToken();

            _userRepository.ResetUserEmailValidationToken(user, validationToken, DateTime.Now.AddMinutes(10));

            SendEmailValidationEmail(user.Email, validationToken);
        }

        public void VerifyEmail(string token)
        {
            User user = _userRepository.GetUserByValidationToken(token);

            if (user.EmailValidationTokenExpiration < DateTime.Now)
            {
                throw new EmailValidationTokenExpiredException("Email validation token expired.");
            }

            _userRepository.SetUserEmailToValidated(user);
        }

        public void GenerateTemporaryPassword(string email)
        {
            User user = _userRepository.GetUserByEmail(email);
            string password = CreateRandomPassword();

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            _userRepository.SetNewPassword(user, passwordHash, passwordSalt);
            SendTemporaryPasswordEmail(email, password);
        }

        public void ResetPassword(string username, string password)
        {
            User user = _userRepository.GetUserByUsername(username);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            _userRepository.SetNewPassword(user, passwordHash, passwordSalt);
        }

        private void SendEmailValidationEmail(string email, string token)
        {
            EmailDto request = new()
            {
                Email = email,
                Subject = "Erősítsd meg az email címed!",
                Body = token
            };
            _emailService.SendEmail(request);
        }

        private void SendTemporaryPasswordEmail(string email, string password)
        {
            EmailDto request = new()
            {
                Email = email,
                Subject = "Ideiglenes jelszó beállítva!",
                Body = $"Az ideiglenes jelszavad: {password} (belépés után rögtön változtasd meg)."
            };
            _emailService.SendEmail(request);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim("Email", user.Email),
                new Claim("Username", user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value
                ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }

        private static string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        private static string CreateRandomPassword()
        {
            string password = "";

            while(!Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
            {
                password = Convert.ToBase64String(RandomNumberGenerator.GetBytes(8));
            }

            return password;
        }
    }
}
