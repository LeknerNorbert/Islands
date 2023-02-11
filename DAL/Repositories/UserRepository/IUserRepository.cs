using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public User GetUserByUsername(string username);
        public User GetUserByEmail(string email);
        public User GetUserByValidationToken(string token);
        public void CreateUser(User user);
        public void UpdateUserPassword(User user, byte[] passwordHash, byte[] passwordSalt);
        public void ResetUserEmailValidationToken(User user, string validationToken, DateTime expiration);
        public void SetUserEmailToValidated(User user);
        public void SetNewPassword(User user, byte[] passwordHash, byte[] passwordSalt);

    }
}
