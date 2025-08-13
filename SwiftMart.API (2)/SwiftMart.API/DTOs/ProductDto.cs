namespace SwiftMart.API.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public bool IsAvailable { get; set; }

        public int Stock { get; set; } // ✅ Add this line

        public string? ImageUrl { get; set; }
    }
}
