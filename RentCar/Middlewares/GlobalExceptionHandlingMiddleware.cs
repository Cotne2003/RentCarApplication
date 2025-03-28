using RentCar.Models;
using System.Net;
using System.Text.Json;

namespace RentCar.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
                _logger.LogError(ex, ex.GetFullMessage());
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var res = context.Response;
            res.ContentType = "application/json";

            var statusCode = ex switch
            {
                ArgumentException => HttpStatusCode.BadRequest,
                KeyNotFoundException => HttpStatusCode.NotFound,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError,
            };

            var errorRes = new ServiceResponse<object>
            {
                StatusCode = statusCode,
                Message = ex.GetFullMessage(),
            };

            await res.WriteAsync(JsonSerializer.Serialize(errorRes));
        }
    }
}
