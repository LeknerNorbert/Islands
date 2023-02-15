using Islands.Models;
using Islands.Models.Context;
using Islands.Models.Enums;

namespace Islands.Repositories.UserRepository
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
            user.Role = Role.User;
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
