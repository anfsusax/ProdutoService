using System.Text.Json;

namespace Infrastructure.Outbox;

public class OutboxEvent
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public string Payload { get; private set; }
    public DateTime OccurredAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }

    public string Status { get; set; }
    private OutboxEvent() { }
    public static OutboxEvent From(object domainEvent)
    {
        return new OutboxEvent
        {
            Id = Guid.NewGuid(),
            Type = domainEvent.GetType().Name,
            Payload = JsonSerializer.Serialize(domainEvent),
            OccurredAt = DateTime.UtcNow,
            Status = "Pending",
        };
    }

    public void MarkAsProcessed()
    {
        ProcessedAt = DateTime.UtcNow;
    }
}
