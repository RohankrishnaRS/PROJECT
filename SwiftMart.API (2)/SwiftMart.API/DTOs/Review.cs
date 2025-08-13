namespace SwiftMart.API.DTOs
{
    public class ReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
    }
}
