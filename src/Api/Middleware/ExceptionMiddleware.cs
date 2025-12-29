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
                _logger.LogWarning(ex, "Erro de domínio");
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { erro = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Registro não encontrado");
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { erro = "Ocorreu um erro interno." });
            }
        }
    }

}
