namespace SwiftMart.API.DTOs
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }

        public CategoryDto Category { get; set; }
        public string SellerName { get; set; }
    }
}
