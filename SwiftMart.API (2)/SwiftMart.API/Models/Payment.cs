using System;
using System.ComponentModel.DataAnnotations;

namespace SwiftMart.API.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public string PaymentStatus { get; set; } // e.g., Success, Failed, Pending

        [Required]
        public DateTime PaymentDate { get; set; }

        public string? TransactionId { get; set; }
    }
}
