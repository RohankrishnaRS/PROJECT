namespace SwiftMart.API.DTOs
{
    public class RegisterRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }  // Acceptable values: "Customer", "Seller", "Admin" (if applicable)
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
