using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Resilience;

public static class EventBusPolicies
{
    public static AsyncRetryPolicy CreateRetryPolicy(ILogger logger)
        => Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    logger.LogWarning(
                        exception,
                        "Retry {RetryCount} ao publicar evento. Aguardando {Delay}s",
                        retryCount,
                        timeSpan.TotalSeconds);
                });

    public static AsyncCircuitBreakerPolicy CreateCircuitBreakerPolicy(ILogger logger)
        => Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (exception, breakDelay) =>
                {
                    logger.LogError(
                        exception,
                        "Circuit aberto por {Delay}s ao publicar eventos",
                        breakDelay.TotalSeconds);
                },
                onReset: () =>
                {
                    logger.LogInformation("Circuit fechado. Publicação de eventos normalizada.");
                });
}
