using Microsoft.AspNetCore.Identity;

namespace SwiftMart.API.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        // Navigation
        public ICollection<Product>? SellerProducts { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
