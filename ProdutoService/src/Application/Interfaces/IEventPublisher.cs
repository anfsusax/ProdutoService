namespace Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync(Object @event);
    }
}
