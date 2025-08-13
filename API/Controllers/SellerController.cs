using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using SwiftMart.API.DTOs;
using SwiftMart.API.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SwiftMart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Seller")]
    public class SellerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SellerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("products/{sellerId}")]
        public async Task<IActionResult> GetMyProducts(int sellerId)
        {
            var products = await _context.Products
                .Where(p => p.SellerId == sellerId)
                .ToListAsync();

            return Ok(products);
        }

        [HttpPost("product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDto requestDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            int sellerId = int.Parse(userIdClaim.Value);

            var product = new Product
            {
                Name = requestDto.Name,
                Description = requestDto.Description,
                Price = requestDto.Price,
                Stock = requestDto.Stock,
                ImageUrl = requestDto.ImageUrl,
                CategoryId = requestDto.CategoryId,
                SellerId = sellerId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet("orders/{sellerId}")]
        public async Task<IActionResult> GetOrdersForSeller(int sellerId)
        {
            var orders = await _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .Where(oi => oi.Product.SellerId == sellerId)
                .ToListAsync();

            return Ok(orders);
        }
    }
}
