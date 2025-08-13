using System;

namespace SwiftMart.API.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }  // e.g., 1 to 5
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserFullName { get; set; }  // Optional, useful in responses to show who reviewed
    }
}
