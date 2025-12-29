using Infrastructure.Persistence;
using Microsoft.Extensions.Diagnostics.HealthChecks;


namespace Api.HealthChecks
{
  
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly ProdutoDbContext _context;

        public DatabaseHealthCheck(ProdutoDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);

                return canConnect
                    ? HealthCheckResult.Healthy("Banco de dados acessível.")
                    : HealthCheckResult.Unhealthy("Não foi possível conectar ao banco.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Erro ao acessar o banco.", ex);
            }
        }
    }

}
