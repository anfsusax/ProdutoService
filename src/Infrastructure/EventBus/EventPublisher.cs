using Application.Interfaces;
using Infrastructure.Resilience;
using Microsoft.Extensions.Logging;
using Polly;

public class EventPublisher : IEventPublisher
{
    private readonly ILogger<EventPublisher> _logger;
    private readonly AsyncPolicy _policy;

    public EventPublisher(ILogger<EventPublisher> logger)
    {
        _logger = logger;

        var retry = EventBusPolicies.CreateRetryPolicy(_logger);
        var circuit = EventBusPolicies.CreateCircuitBreakerPolicy(_logger);

        _policy = Policy.WrapAsync(retry, circuit);
    }

    public async Task PublishAsync(Object @event)
    {
        try
        {
            _logger.LogInformation(
                "Publicando evento {EventName}",
                @event.GetType().Name);

            // Simulação de envio (futuro: Kafka, RabbitMQ etc.)
            await Task.Run(() =>
            {
                Console.WriteLine($"Evento publicado: {@event.GetType().Name}");
            });

            _logger.LogInformation(
                "Evento publicado com sucesso {EventName}",
                @event.GetType().Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Falha ao publicar evento {EventName}",
                @event.GetType().Name);

            throw;
        }
    }
}
