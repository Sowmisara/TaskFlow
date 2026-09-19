using FluentValidation;
using System.Text.Json;
using TaskFlow.API.ExceptionModel;

namespace TaskFlow.API.Middleware
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
            catch(ValidationException ex)
            {
                _logger.LogWarning("ValidationErrros : {Message}",ex.Message);
                var errors = new ErrorResponse {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Validation failed",
                    Errors = ex.Errors.Select(x => x.ErrorMessage).ToList()
                };
                await WriteErrorResponseAsync(context, errors);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Unhandled exception occurred",ex.Message);
                var errors = new ErrorResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An unexpected error occurred. Please try again later.",
                    Errors = new List<string>()
                };
                await WriteErrorResponseAsync(context,errors);
            }
        }

        public async Task WriteErrorResponseAsync(HttpContext context,ErrorResponse errors)
        {
            context.Response.StatusCode = errors.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(errors, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
    }
}
