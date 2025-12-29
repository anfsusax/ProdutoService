using Application.Interfaces;

namespace Infrastructure.EventBus
{
   
    public class EventPublisher : IEventPublisher
    { 
        public async Task PublishAsync<TEvent>(TEvent @event)
        { 
            await Task.Run(() =>
            {
                Console.WriteLine($"Evento publicado: {@event.GetType().Name}");
            });
        }
    }

}
