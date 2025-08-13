namespace SwiftMart.API.DTOs
{
    public class PaymentRequestDto
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        // Add other properties like payment details if needed
    }
}
