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
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByValidationTokenAsync(string token);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<DateTime> GetEmailValidationDateByUsernameAsync(string username);
    }
}
