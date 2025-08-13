namespace SwiftMart.API.DTOs
{
    public class ProductRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int Stock { get; set; }

        public string? ImageUrl { get; set; }

        public string SellerId { get; set; }  // Required for associating the product to a seller
    }
}
