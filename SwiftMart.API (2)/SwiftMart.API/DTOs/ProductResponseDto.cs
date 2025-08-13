namespace SwiftMart.API.DTOs
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool IsAvailable { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public string SellerId { get; set; }
        public string SellerName { get; set; } // Optional, just to show seller info
    }
}
