using BLL.Exceptions;
using BLL.Services.EmailService;
using DAL.DTOs;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.UserRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(
            IUserRepository userRepository, 
            IConfiguration configuration, 
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<string> LoginAsync(LoginRequestDto login)
        {
            User user = await _userRepository.GetUserByUsernameAsync(login.Username);

            if (VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                return CreateToken(user);
            }

            throw new LoginValidationException("Credentials are incorrect.");
        }

        public async Task RegistrationAsync(RegistrationRequestDto registration)
        {
            CreatePasswordHash(registration.Password, out byte[] passwordHash, out byte[] passwordSalt);
            string validationToken = CreateRandomToken();

            User user = new()
            {
                Username = registration.Username,
                Email = registration.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                EmailValidationToken = validationToken,
                EmailValidationTokenExpiration = DateTime.Now.AddMinutes(10)
            };

            await _userRepository.AddUserAsync(user);
            SendEmailValidationEmail(user.Email, validationToken);
        }

        public async Task ResendVerifyEmailAsync(string username)
        {
            User user = await _userRepository.GetUserByEmailAsync(username);

            string validationToken = CreateRandomToken();
            user.EmailValidationToken = validationToken;
            user.EmailValidationTokenExpiration = DateTime.Now.AddMinutes(10);

            await _userRepository.UpdateUserAsync(user);

            SendEmailValidationEmail(user.Email, validationToken);
        }

        public async Task<bool> VerifyEmailAsync(string token)
        {
            User user = await _userRepository.GetUserByValidationTokenAsync(token);

            if (user.EmailValidationTokenExpiration >= DateTime.Now)
            {
                user.EmailValidationDate = DateTime.Now;
                await _userRepository.UpdateUserAsync(user);

                return true;
            }

            return false;
        }

        public async Task SetTemporaryPasswordAsync(string email)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            string password = CreateRandomPassword();

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.UpdateUserAsync(user);

            SendTemporaryPasswordEmail(email, password);
        }

        public async Task UpdatePasswordAsync(string username, PasswordChangeDto password)
        {
            User user = await _userRepository.GetUserByUsernameAsync(username);
            CreatePasswordHash(password.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.UpdateUserAsync(user);
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
                new Claim("Username", user.Username)
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

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
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

            while (!Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
            {
                password = Convert.ToBase64String(RandomNumberGenerator.GetBytes(8));
            }

            return password;
        }
    }
}
