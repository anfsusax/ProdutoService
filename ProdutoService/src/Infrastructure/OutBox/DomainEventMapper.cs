using System.Text.Json;
using Infrastructure.Outbox;

namespace Infrastructure.OutBox;

public static class DomainEventMapper
{
    public static OutboxEvent ToOutboxEvent(object domainEvent)
    {
        var type = domainEvent.GetType().Name;
        var payload = JsonSerializer.Serialize(domainEvent);

        return new OutboxEvent(type, payload);
    }
}
