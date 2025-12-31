namespace Application.Interfaces;

public interface IOutboxWriter
{
    Task AddAsync(object domainEvent);
}
