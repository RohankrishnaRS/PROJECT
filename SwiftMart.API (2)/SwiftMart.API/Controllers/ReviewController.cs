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
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existing = await _context.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ProductId == dto.ProductId);

            if (existing != null)
                return BadRequest("You already reviewed this product.");

            var review = new Review
            {
                Rating = dto.Rating,
                Comment = dto.Comment,
                ProductId = dto.ProductId,
                UserId = userId
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok("Review added.");
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetProductReviews(int productId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .Select(r => new {
                    r.Rating,
                    r.Comment,
                    User = r.User.FullName,
                    r.CreatedAt
                })
                .ToListAsync();

            return Ok(reviews);
        }
    }
}
