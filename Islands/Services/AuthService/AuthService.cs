
using Islands.DTOs;
using Islands.Exceptions;
using Islands.Models;
using Islands.Models.Enums;
using Islands.Repositories.UserRepository;
using Islands.Services.EmailService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Islands.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;

        public AuthService(IUserRepository userRepository,  IConfiguration configuration, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.emailService = emailService;
        }

        public async Task<string> LoginAsync(LoginRequestWithUsernameDto login)
        {
            try
            {
                User user = await userRepository.GetByUsernameAsync(login.Username);

                if (VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return CreateToken(user);
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to get user by username.", ex);
            }

            throw new LoginFailedException("Credentials are incorrect.");
        }

        public async Task RegistrationAsync(RegistrationRequestDto registration)
        {
            try
            {
                CreatePasswordHash(registration.Password, out byte[] passwordHash, out byte[] passwordSalt);
                string validationToken = CreateRandomToken();

                User user = new()
                {
                    Username = registration.Username,
                    Email = registration.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = Role.Guest,
                    EmailValidationToken = validationToken,
                    EmailValidationTokenExpiration = DateTime.Now.AddMinutes(10)
                };

                await userRepository.AddAsync(user);
                SendEmailValidationEmail(user.Email, validationToken);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to create user.", ex);
            }
        }

        public async Task ResendVerifyEmailAsync(string username)
        {
            try
            {
                User user = await userRepository.GetByUsernameAsync(username);

                string validationToken = CreateRandomToken();
                user.EmailValidationToken = validationToken;
                user.EmailValidationTokenExpiration = DateTime.Now.AddMinutes(10);

                await userRepository.UpdateAsync(user);

                SendEmailValidationEmail(user.Email, validationToken);
            }
            catch (Exception ex)
            {
                throw new ServiceException("There is no user with this email.", ex);
            }
        }

        public async Task<bool> VerifyEmailAsync(string token)
        {
            try
            {
                User user = await userRepository.GetByValidationTokenAsync(token);

                if (user.EmailValidationTokenExpiration >= DateTime.Now)
                {
                    user.EmailValidationDate = DateTime.Now;
                    await userRepository.UpdateAsync(user);

                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to update user.", ex);
            }
        }

        public async Task SetTemporaryPasswordAsync(string email)
        {
            try
            {
                User user = await userRepository.GetByEmailAsync(email);
                string password = CreateRandomPassword();

                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await userRepository.UpdateAsync(user);

                SendTemporaryPasswordEmail(email, password);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to update user.", ex);
            }
        }

        public async Task UpdatePasswordAsync(string username, PasswordResetDto password)
        {
            try
            {
                User user = await userRepository.GetByUsernameAsync(username);
                CreatePasswordHash(password.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to update user.", ex);
            }
        }

        private void SendEmailValidationEmail(string email, string token)
        {
            EmailDto request = new()
            {
                Email = email,
                Subject = "Erősítsd meg az email címed!",
                Body = token
            };
            emailService.SendEmail(request);
        }

        private void SendTemporaryPasswordEmail(string email, string password)
        {
            EmailDto request = new()
            {
                Email = email,
                Subject = "Ideiglenes jelszó beállítva!",
                Body = $"Az ideiglenes jelszavad: {password} (belépés után rögtön változtasd meg)."
            };
            emailService.SendEmail(request);
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
                    configuration.GetSection("AppSettings:Token").Value
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

            while(!Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
            {
                password = Convert.ToBase64String(RandomNumberGenerator.GetBytes(8));
            }

            return password;
        }
    }
}
