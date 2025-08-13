// OrderResponseDto.cs
namespace SwiftMart.API.DTOs
{
    public class OrderItemResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }  // optional
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponseDto> OrderItems { get; set; }
    }
}
