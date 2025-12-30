using Application.Interfaces;
using Infrastructure.EventBus;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Produto.Infrastructure.Repositories;

namespace Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ProdutoDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
 
        services.AddScoped<EventPublisher>();
 
        services.AddScoped<IEventPublisher>(sp =>
        {
            var inner = sp.GetRequiredService<EventPublisher>();
            var logger = sp.GetRequiredService<ILogger<EventPublisherRetryDecorator>>();

            return new EventPublisherRetryDecorator(inner, logger);
        });

        return services;
    }
}
