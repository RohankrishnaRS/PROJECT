using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using SwiftMart.API.DTOs;          // Make sure to include this
using SwiftMart.API.Helpers;
using SwiftMart.API.Models;
using System;
using System.Threading.Tasks;

namespace SwiftMart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;
        public PaymentController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] PaymentRequestDto requestDto)
        {
            // ... existing payment saving logic ...

            var payment = new Payment
            {
                OrderId = requestDto.OrderId,
                PaymentMethod = requestDto.PaymentMethod,
                Amount = requestDto.Amount,
                PaidAt = DateTime.UtcNow,
                Status = "Paid"
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Fetch the order along with user info for email
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == payment.OrderId);

            if (order?.User != null)
            {
                var subject = $"Your Order #{order.Id} Confirmation - QuitQ";
                var body = $"<h2>Dear {order.User.FullName},</h2>" +
                           $"<p>Your payment of {payment.Amount:C} has been received successfully.</p>" +
                           $"<p>Order Details:</p>" +
                           $"<ul>" +
                           string.Join("", order.OrderItems.Select(i => $"<li>{i.Product.Name} - Qty: {i.Quantity} - Price: {i.Price:C}</li>")) +
                           $"</ul>" +
                           $"<p>Shipping Address: {order.ShippingAddress}</p>" +
                           $"<p>Thank you for shopping with QuitQ!</p>";

                await _emailService.SendEmailAsync(order.User.Email, subject, body);
            }

            return Ok(payment);
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
