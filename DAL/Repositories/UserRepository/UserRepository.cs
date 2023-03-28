using DAL.Models;
using DAL.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByValidationTokenAsync(string token)
        {
            return await _context.Users.FirstAsync(u => u.EmailValidationToken == token);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<DateTime> GetEmailValidationDateByUsernameAsync(string username)
        {
            return await _context.Users
                .Where(user => user.Username == username)
                .Select(user => user.EmailValidationDate)
                .FirstAsync();
        }
    }
}
