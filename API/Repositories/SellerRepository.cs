using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using SwiftMart.API.Interfaces;
using SwiftMart.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMart.API.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ApplicationDbContext _context;

        public SellerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddSellerAsync(User seller)
        {
            await _context.Users.AddAsync(seller);
            await _context.SaveChangesAsync();
        }

        // METHOD NAME FIX: matches ISellerRepository
        public async Task DeleteSellerAsync(int sellerId)
        {
            var seller = await _context.Users.FindAsync(sellerId);
            if (seller != null && seller.Role == "Seller")
            {
                _context.Users.Remove(seller);
                await _context.SaveChangesAsync();
            }
        }

        // METHOD NAME FIX: matches ISellerRepository
        public async Task<IEnumerable<User>> GetAllSellersAsync()
        {
            return await _context.Users
                .Where(u => u.Role == "Seller")
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int sellerId)
        {
            var seller = await _context.Users.FindAsync(sellerId);
            return (seller != null && seller.Role == "Seller") ? seller : null;
        }

        // METHOD NAME FIX: matches ISellerRepository
        public async Task UpdateSellerAsync(User seller)
        {
            _context.Users.Update(seller);
            await _context.SaveChangesAsync();
        }
    }
}
