using Pedido.Domain.Exceptions;

namespace Pedido.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
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
            _logger.LogWarning(
                ex,
                "Erro de domínio em {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(
                ex,
                "Recurso não encontrado em {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { erro = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(
                ex,
                "Operação inválida em {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Erro inesperado em {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(
                new { erro = "Ocorreu um erro interno." });
        }
    }
}

