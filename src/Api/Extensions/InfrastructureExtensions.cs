using Application.Interfaces;

namespace Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddScoped<IEventPublisher, EventPublisher>();
        return services;
    }
}
