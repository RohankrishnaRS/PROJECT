using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using SwiftMart.API.Interfaces;
using SwiftMart.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMart.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Corrected method name matching interface
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // You can keep a generic get all users if interface has it, but interface seems to want customers only
        // Implement this method if desired - or leave it out if interface doesn't declare

        // Corrected method name matching interface
        public async Task<IEnumerable<User>> GetAllCustomersAsync()
        {
            return await _context.Users.Where(u => u.Role == "Customer").ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Corrected method name matching interface
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
