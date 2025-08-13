using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using System.Threading.Tasks;

namespace SwiftMart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetCustomers()
        {
            var users = await _context.Users
                .AsNoTracking()
                .Where(u => u.Role == "Customer")
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("sellers")]
        public async Task<IActionResult> GetSellers()
        {
            var sellers = await _context.Users
                .AsNoTracking()
                .Where(u => u.Role == "Seller")
                .ToListAsync();

            return Ok(sellers);
        }

        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User deleted" });
        }

        [HttpDelete("seller/{sellerId}")]
        public async Task<IActionResult> DeleteSeller(int sellerId)
        {
            var seller = await _context.Users.FindAsync(sellerId);
            if (seller == null) return NotFound();

            _context.Users.Remove(seller);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Seller deleted" });
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .AsNoTracking()
                .ToListAsync();

            return Ok(products);
        }
    }
}
