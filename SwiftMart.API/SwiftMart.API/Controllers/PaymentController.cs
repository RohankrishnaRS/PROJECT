using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using SwiftMart.API.DTOs;          // Make sure to include this
using SwiftMart.API.Models;
using System.Threading.Tasks;
using System;

namespace SwiftMart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] PaymentRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var payment = new Payment
            {
                OrderId = requestDto.OrderId,
                PaymentMethod = requestDto.PaymentMethod,
                Amount = requestDto.Amount,
                PaidAt = DateTime.UtcNow,
                Status = "Paid" // Or manage status based on your logic
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(payment);  // You can map to a Response DTO if desired
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetPayment(int orderId)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
            if (payment == null) return NotFound();

            return Ok(payment);
        }
    }
}
