using Domain.Exceptions;

namespace Api.Middleware
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
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, "Erro de domínio: {Message}", ex.Message);
                await WriteErrorResponse(context, 400, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Registro não encontrado: {Message}", ex.Message);
                await WriteErrorResponse(context, 404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado: {Message}", ex.Message);
                await WriteErrorResponse(context, 500, "Ocorreu um erro interno.");
            }
        }

        private static Task WriteErrorResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(new { erro = message });
        }
    }
}
