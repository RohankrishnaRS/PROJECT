using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftMart.API.Models;

namespace SwiftMart.API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllCustomersAsync();
        Task<User> GetByIdAsync(int userId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
    }
}
