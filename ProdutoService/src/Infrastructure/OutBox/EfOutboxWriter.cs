

using Application.Interfaces;
using Infrastructure.Outbox;
using Infrastructure.Persistence;

namespace Infrastructure.OutBox;

public class EfOutboxWriter : IOutboxWriter
{
    private readonly ProdutoDbContext _context;

    public EfOutboxWriter(ProdutoDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(object domainEvent)
    {
        var outboxEvent = OutboxEvent.From(domainEvent);
        await _context.OutboxEvents.AddAsync(outboxEvent);

    }
}
