using Islands.Models;

namespace Islands.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByValidationTokenAsync(string token);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
