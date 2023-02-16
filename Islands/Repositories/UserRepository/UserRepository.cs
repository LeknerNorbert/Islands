using Islands.Exceptions;
using Islands.Models;
using Islands.Models.Context;
using Islands.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Islands.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding user.", ex);
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstAsync(u => u.Email == email);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstAsync(u => u.Username == username);
        }

        public async Task<User> GetByValidationTokenAsync(string token)
        {
            return await _context.Users.FirstAsync(u => u.EmailValidationToken == token);
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating user.", ex);
            }
        }

    }
}
