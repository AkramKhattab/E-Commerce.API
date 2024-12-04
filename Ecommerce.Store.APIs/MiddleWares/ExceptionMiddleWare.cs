using Ecommerce.Store.APIs.Errors;
using System.Text.Json;

namespace Ecommerce.Store.APIs.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = _env.IsDevelopment() ?
                    new ApiExceptionResponse(StatusCodes.Status500InternalServerError, ex.Message, ex?.StackTrace?.ToString())
                  : new ApiExceptionResponse(StatusCodes.Status500InternalServerError);

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

    }
}
