using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SwiftMart.API.Data;
using SwiftMart.API.Models;

namespace SwiftMart.API.Helpers
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Only seed if users table is empty or roles not present
            bool hasAdmin = await context.Users.AnyAsync(u => u.Role == "Admin");
            bool hasSeller = await context.Users.AnyAsync(u => u.Role == "Seller");
            bool hasCustomer = await context.Users.AnyAsync(u => u.Role == "Customer");

            if (!hasAdmin)
            {
                var admin = new User
                {
                    FullName = "Administrator",
                    Email = "admin@quitq.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), // Change this in production!
                    Role = "Admin",
                    Address = "Headquarters",
                    Phone = "0000000000"
                };
                context.Users.Add(admin);
            }

            if (!hasSeller)
            {
                var seller = new User
                {
                    FullName = "Default Seller",
                    Email = "seller@quitq.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Seller@123"),
                    Role = "Seller",
                    Address = "Seller Office",
                    Phone = "1111111111"
                };
                context.Users.Add(seller);
            }

            if (!hasCustomer)
            {
                var customer = new User
                {
                    FullName = "Default Customer",
                    Email = "customer@quitq.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Customer@123"),
                    Role = "Customer",
                    Address = "Customer Address",
                    Phone = "2222222222"
                };
                context.Users.Add(customer);
            }

            await context.SaveChangesAsync();
        }
    }
}
