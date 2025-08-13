using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using SwiftMart.API.DTOs;
using SwiftMart.API.Models;
using System.Security.Claims;

namespace SwiftMart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? category,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] bool? isAvailable)
        {
            var query = _context.Products
                .Include(p => p.Seller)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search));

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category == category);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (isAvailable.HasValue)
                query = query.Where(p => p.IsAvailable == isAvailable.Value);

            var products = await query
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Category = p.Category,
                    IsAvailable = p.IsAvailable,
                    Stock = p.Stock,
                    ImageUrl = p.ImageUrl,
                    SellerId = p.SellerId,
                    SellerName = p.Seller.FullName
                })
                .ToListAsync();

            return Ok(products);
        }

        // ✅ CREATE PRODUCT (Admin or Seller)
        [HttpPost]
        [Authorize(Roles = "Admin, Seller")]
        public async Task<IActionResult> Create(ProductRequestDto request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = request.Category,
                IsAvailable = request.IsAvailable,
                Stock = request.Stock,
                ImageUrl = request.ImageUrl,
                SellerId = request.SellerId
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                IsAvailable = product.IsAvailable,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                SellerId = product.SellerId,
                SellerName = product.Seller?.FullName
            });
        }

        // ✅ UPDATE PRODUCT
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Seller")]
        public async Task<IActionResult> Update(int id, ProductRequestDto request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Category = request.Category;
            product.IsAvailable = request.IsAvailable;
            product.Stock = request.Stock;
            product.ImageUrl = request.ImageUrl;
            product.SellerId = request.SellerId;

            await _context.SaveChangesAsync();

            return Ok(new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                IsAvailable = product.IsAvailable,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                SellerId = product.SellerId,
                SellerName = product.Seller?.FullName
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}
