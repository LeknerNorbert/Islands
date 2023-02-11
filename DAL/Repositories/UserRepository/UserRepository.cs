using DAL.Models;
using DAL.Models.Context;
using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.First(u => u.Email == email);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.First(u => u.Username == username);
        }

        public User GetUserByValidationToken(string token)
        {
            return _context.Users.First(u => u.EmailValidationToken == token);
        }

        public void UpdateUserPassword(User user, byte[] passwordHash, byte[] passwordSalt)
        {
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.SaveChanges();
        }

        public void ResetUserEmailValidationToken(User user, string validationToken, DateTime expiration)
        {
            user.EmailValidationToken = validationToken;
            user.EmailValidationTokenExpiration = expiration;
            _context.SaveChanges();
        }

        public void SetUserEmailToValidated(User user)
        {
            user.Role = RoleType.User;
            _context.SaveChanges();
        }

        public void SetNewPassword(User user, byte[] passwordHash, byte[] passwordSalt)
        {
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.SaveChanges();
        }

    }
}
