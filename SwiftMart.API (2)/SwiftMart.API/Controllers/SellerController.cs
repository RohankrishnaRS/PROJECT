using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using SwiftMart.API.DTOs;
using SwiftMart.API.Models;
using System.Security.Claims;

namespace SwiftMart.API.Controllers
{
    [Authorize(Roles = "Seller")]
    [ApiController]
    [Route("api/[controller]")]
    public class SellerController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public SellerController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // POST: api/seller/add-product
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Category = dto.Category,
                IsAvailable = true,
                Stock = dto.Stock,
                SellerId = userId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product added successfully", product });
        }

        // PUT: api/seller/update-product/5
        [HttpPut("update-product/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.SellerId == userId);
            if (product == null)
                return NotFound("Product not found or unauthorized.");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Category = dto.Category;
            product.Stock = dto.Stock;
            product.IsAvailable = dto.IsAvailable;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Product updated successfully", product });
        }

        // GET: api/seller/my-products
        [HttpGet("my-products")]
        public async Task<IActionResult> GetMyProducts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var products = await _context.Products
                .Where(p => p.SellerId == userId)
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/seller/my-orders
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetOrdersForMyProducts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Items.Any(i => i.Product.SellerId == userId))
                .ToListAsync();

            return Ok(orders);
        }
    }
}
