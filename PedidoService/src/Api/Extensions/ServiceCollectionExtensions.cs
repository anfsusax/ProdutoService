using Microsoft.EntityFrameworkCore;
using Pedido.Application.Commands.Pedidos.CriarPedido;
using Pedido.Application.Interfaces;
using Pedido.Infrastructure.Persistence;
using Pedido.Infrastructure.Repositories;

namespace Pedido.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Handlers
        services.AddScoped<CriarPedidoHandler>();

        // Repositórios
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PedidoDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}
