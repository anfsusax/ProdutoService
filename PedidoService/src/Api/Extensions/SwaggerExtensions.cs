using Microsoft.OpenApi.Models;

namespace Pedido.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Pedido API",
                Version = "v1.0",
                Description = "API para gerenciamento de pedidos."
            });
        });

        return services;
    }
}

