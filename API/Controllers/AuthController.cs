using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using SwiftMart.API.DTOs;
using SwiftMart.API.Helpers;
using SwiftMart.API.Interfaces;
using SwiftMart.API.Models;
using System.Threading.Tasks;

namespace SwiftMart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest(new { message = "Email already registered." });

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                Address = request.Address,
                Phone = request.Phone
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid email or password" });

            var token = _tokenService.GenerateJwtToken(user);
            return Ok(new { token, user = new { user.Id, user.FullName, user.Email, user.Role } });
        }
    }
}
