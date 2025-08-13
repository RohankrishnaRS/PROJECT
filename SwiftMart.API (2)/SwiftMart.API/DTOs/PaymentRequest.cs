namespace SwiftMart.API.DTOs
{
    public class PaymentRequest
    {
        public int OrderId { get; set; }
        public string? TransactionId { get; set; } // Optional for mock
    }
}
