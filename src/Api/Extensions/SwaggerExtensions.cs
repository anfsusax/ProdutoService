using Microsoft.OpenApi;

namespace Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Produto API",
                    Version = "v1.1",
                    Description = "API para gerenciamento de produtos."
                });
            });

            services.AddOpenApi();  
            return services;
        }
    }
}
 
