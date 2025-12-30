using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Common.Handlers
{
    public abstract class BaseHandler<TEntity>
    where TEntity : class
    {
        protected readonly ILogger _logger;
        protected readonly IEventPublisher _eventPublisher;

        protected BaseHandler(ILogger logger, IEventPublisher eventPublisher)
        {
            _logger = logger;
            _eventPublisher = eventPublisher;
        }

        protected async Task HandleDomainEventsAsync(TEntity entity, Func<Task> action, IEnumerable<object>? domainEvents = null)
        {
            try
            {
                await action();

                if (domainEvents != null)
                {
                    foreach (var e in domainEvents)
                        await _eventPublisher.PublishAsync(e);
                }
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, "Erro de domínio: {Message}", ex.Message);
                throw;
            }
        }

    }

}
