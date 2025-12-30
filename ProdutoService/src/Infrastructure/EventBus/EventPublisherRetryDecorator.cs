using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Infrastructure.EventBus
{
    public class EventPublisherRetryDecorator : IEventPublisher
    {
        private readonly IEventPublisher _inner;
        private readonly ILogger<EventPublisherRetryDecorator> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;
        public EventPublisherRetryDecorator(
       IEventPublisher inner,
       ILogger<EventPublisherRetryDecorator> logger)
        {
            _inner = inner;
            _logger = logger;

            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: attempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(
                            exception,
                            "Falha ao publicar evento. Tentativa {RetryAttempt}. Nova tentativa em {Delay}s",
                            retryCount,
                            timeSpan.TotalSeconds);
                    });
        }

        public async Task PublishAsync(object @event)
        {
            await _retryPolicy.ExecuteAsync(() =>
                _inner.PublishAsync(@event));
        }
    }
}