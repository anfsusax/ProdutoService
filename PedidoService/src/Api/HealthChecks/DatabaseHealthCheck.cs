using Microsoft.Extensions.Diagnostics.HealthChecks;
using Pedido.Infrastructure.Persistence;

namespace Pedido.Api.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly PedidoDbContext _context;

    public DatabaseHealthCheck(PedidoDbContext context)
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

