using System.Net;
using System.Text.Json;
using ContactmgmtWebAPI.Models;


namespace ContactmgmtWebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                Timestamp = DateTime.UtcNow,
                Message = ex.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            // Custom logic for different exception types
            if (ex is Exceptions.ContactNotFoundException)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (ex is Microsoft.Data.SqlClient.SqlException)
            {
                response.Message = "Database operation failed.";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            context.Response.StatusCode = response.StatusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
