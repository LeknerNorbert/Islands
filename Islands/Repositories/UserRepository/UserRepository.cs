using Islands.Exceptions;
using Islands.Models;
using Islands.Models.Context;
using Islands.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Islands.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task AddAsync(User user)
        {
            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding user.", ex);
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await context.Users.FirstAsync(u => u.Email == email);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await context.Users.FirstAsync(u => u.Username == username);
        }

        public async Task<User> GetByValidationTokenAsync(string token)
        {
            return await context.Users.FirstAsync(u => u.EmailValidationToken == token);
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating user.", ex);
            }
        }

    }
}
