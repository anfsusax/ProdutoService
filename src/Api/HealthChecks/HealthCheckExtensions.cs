using Microsoft.Extensions.DependencyInjection;

namespace Api.HealthChecks
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthChecksConfiguration(
            this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<DatabaseHealthCheck>(
                    "database",
                    tags: new[] { "ready" });

            return services;
        }
    }
}
