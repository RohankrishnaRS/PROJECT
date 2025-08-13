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
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/Order
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Extract the user id from JWT claims instead of trusting the client
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            // Create Order entity
            var order = new Order
            {
                UserId = userId,
                ShippingAddress = requestDto.ShippingAddress,
                TotalAmount = requestDto.TotalAmount,
                Status = requestDto.Status,
                CreatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            // Map order items
            foreach (var itemDto in requestDto.OrderItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Price = itemDto.Price
                };
                order.OrderItems.Add(orderItem);
            }

            // Save to DB
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Map saved entity to response DTO
            var responseDto = new OrderResponseDto
            {
                Id = order.Id,
                UserId = order.UserId,
                ShippingAddress = order.ShippingAddress,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name, // May be null if not loaded
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, responseDto);
        }

        // GET api/Order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            var responseDto = new OrderResponseDto
            {
                Id = order.Id,
                UserId = order.UserId,
                ShippingAddress = order.ShippingAddress,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                CreatedAt = order.CreatedAt,
                OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            return Ok(responseDto);
        }
    }
}
