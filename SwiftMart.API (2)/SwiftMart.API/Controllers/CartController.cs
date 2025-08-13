using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using SwiftMart.API.Models;
using System.Security.Claims;

namespace SwiftMart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CartController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.OrderId == 0 && c.Product != null && c.Product.IsAvailable && c.Order == null)
                .Where(c => c.Product != null && c.Product.Id > 0)
                .ToListAsync();

            return Ok(cartItems);
        }

        // POST: api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromQuery] int productId, [FromQuery] int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _context.Products.FindAsync(productId);
            if (product == null || !product.IsAvailable)
                return BadRequest("Invalid or unavailable product.");

            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == productId && c.OrderId == 0 && c.Order == null);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price,
                    OrderId = 0 // indicates it's not ordered yet
                });
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Item added to cart." });
        }

        // PUT: api/cart/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateQuantity([FromQuery] int cartItemId, [FromQuery] int quantity)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item == null) return NotFound("Cart item not found.");

            item.Quantity = quantity;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Quantity updated." });
        }

        // DELETE: api/cart/remove/{id}
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound("Cart item not found.");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Item removed from cart." });
        }
    }
}