using global::Api.HealthChecks;


namespace Api.Extensions
{
   
    namespace Api.Extensions
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

}

