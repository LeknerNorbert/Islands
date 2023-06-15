using BLL.Exceptions;
using BLL.Services.EmailService;
using DAL.DTOs;
using DAL.Models;
using DAL.Repositories.UserRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BLL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly string rootPath;

        public AuthService(
            IUserRepository userRepository, 
            IConfiguration configuration, 
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _emailService = emailService;
            rootPath = _configuration.GetSection("RootPath").Value;
        }

        public async Task<string> LoginAsync(LoginRequestDto login)
        {
            User user = await _userRepository.GetUserByUsernameAsync(login.Username);

            if (VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                if (user.EmailValidationDate == DateTime.MinValue)
                {
                    throw new EmailNotValidatedException("Email is not valited.");
                }

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
                EmailValidationTokenExpiration = DateTime.Now.AddMinutes(10),
                EmailValidationDate = DateTime.Now
            };

            await _userRepository.AddUserAsync(user);
            //SendEmailValidationEmail(user.Username, user.Email, validationToken);
        }

        public async Task ResendVerifyEmailAsync(string email)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);

            string validationToken = CreateRandomToken();
            user.EmailValidationToken = validationToken;
            user.EmailValidationTokenExpiration = DateTime.Now.AddMinutes(10);

            await _userRepository.UpdateUserAsync(user);

            SendEmailValidationEmail(user.Username, user.Email, validationToken);
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

            SendTemporaryPasswordEmail(user.Username, email, password);
        }

        public async Task UpdatePasswordAsync(string username, PasswordChangeDto password)
        {
            User user = await _userRepository.GetUserByUsernameAsync(username);
            CreatePasswordHash(password.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.UpdateUserAsync(user);
            SendEmailPasswordChangeEmail(user.Username, user.Email);
        }

        public async Task<DateTime> GetEmailValidationDateByUsernameAsync(string username)
        {
            DateTime emailValidationDate = await _userRepository.GetEmailValidationDateByUsernameAsync(username);

            if (emailValidationDate == DateTime.MinValue)
            {
                throw new EmailNotValidatedException("Email is not validated.");
            }

            return emailValidationDate; 
        }

        public string GetTokenForNotificationHub()
        {
            User user = new()
            {
                Username = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString()
            };
            return CreateToken(user);
        }

        private void SendEmailValidationEmail(string username, string email, string token)
        {
            string path = Path.Combine(rootPath, "Templates", "EmailValidationEmail.html");
            string htmlTemplate = File.ReadAllText(path);
            htmlTemplate = htmlTemplate.Replace("Username", username);
            htmlTemplate = htmlTemplate.Replace("Token", token);

            EmailDto request = new()
            {
                Email = email,
                Subject = "Erősítsd meg az email címed!",
                Body = htmlTemplate
            };

            _emailService.SendEmail(request);
        }

        private void SendEmailPasswordChangeEmail(string username, string email) 
        {
            string path = Path.Combine(rootPath, "Templates", "UpdatePasswordEmail.html");
            string htmlTemplate = File.ReadAllText(path);
            htmlTemplate = htmlTemplate.Replace("Username", username);

            EmailDto request = new()
            {
                Email = email,
                Subject = "Jelszavad megváltozott!",
                Body = htmlTemplate
            };

            _emailService.SendEmail(request);
        }

        private void SendTemporaryPasswordEmail(string username, string email, string password)
        {
            string path = Path.Combine(rootPath, "Templates", "TemporaryPasswordEmail.html");
            string htmlTemplate = File.ReadAllText(path);
            htmlTemplate = htmlTemplate.Replace("Username", username);
            htmlTemplate = htmlTemplate.Replace("<span>TemporaryPassword</span>", $"<span>{password}</span>");

            EmailDto request = new()
            {
                Email = email,
                Subject = "Ideiglenes jelszó beállítva!",
                Body = htmlTemplate,
            };

            _emailService.SendEmail(request);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim("Email", user.Email),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value
                ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

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
