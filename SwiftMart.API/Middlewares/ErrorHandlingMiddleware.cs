using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SwiftMart.API.DTOs;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwiftMart.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log with stack trace for diagnostics
                _logger.LogError(ex, $"An unhandled exception occurred: {ex.Message}");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new ErrorResponseDto
                {
                    Message = "An unexpected error occurred. Please try again later.",
#if DEBUG
                    Details = ex.Message // Only for debugging, remove in production
#endif
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
