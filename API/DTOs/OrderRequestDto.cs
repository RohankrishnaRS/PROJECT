// OrderRequestDto.cs
namespace SwiftMart.API.DTOs
{
    public class OrderItemRequestDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderRequestDto
    {
        // Remove UserId here for better security if you extract from token.
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemRequestDto> OrderItems { get; set; }
    }
}
