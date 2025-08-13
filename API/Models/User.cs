using System.Collections.Generic;

namespace SwiftMart.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }  // "Customer", "Seller", "Admin"

        public string Address { get; set; }

        public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
