using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pedido.Application.Commands.Pedidos.CriarPedido;
using Pedido.Application.Interfaces;
using Pedido.Application.Queries.Pedidos.ObterPedidoPorId;
using Pedido.Application.Queries.Pedidos.ObterTodosPedidos;
using Pedido.Infrastructure.Http;
using Pedido.Infrastructure.Persistence;
using Pedido.Infrastructure.Repositories;

namespace Pedido.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    { 
        services.AddScoped<CriarPedidoHandler>();
        services.AddScoped<ObterPedidoPorIdQueryHandler>();
        services.AddScoped<ObterTodosPedidosQueryHandler>();
         
        services.AddScoped<IPedidoRepository, PedidoRepository>();
         
        services.AddHttpClient<IProdutoCatalogClient, ProdutoCatalogClient>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var logger = sp.GetRequiredService<ILogger<ProdutoCatalogClient>>();
            var baseAddress = configuration["Services:ProdutoService"];

            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new InvalidOperationException("Configuração 'Services:ProdutoService' não encontrada no appsettings.json.");
            }
             
            var uri = new Uri(baseAddress.EndsWith("/") ? baseAddress : $"{baseAddress}/");
            client.BaseAddress = uri;
            client.Timeout = TimeSpan.FromSeconds(30);
            
            // Forçar HTTP/1.1 para melhor compatibilidade no Docker
            client.DefaultRequestVersion = new System.Version(1, 1);
            
            logger.LogInformation("HttpClient configurado para ProdutoService: {BaseAddress}", uri);
        });

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PedidoDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}
