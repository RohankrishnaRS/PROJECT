namespace SwiftMart.API.DTOs
{
    public class ErrorResponseDto
    {
        public string Message { get; set; }
        public string Details { get; set; } // For optional/useful error info, not for user in prod
    }
}
