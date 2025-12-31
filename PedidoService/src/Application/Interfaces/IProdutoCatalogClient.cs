namespace Pedido.Application.Interfaces;

public record ProdutoInfo(Guid Id, string Nome, decimal Preco, bool Ativo);

public interface IProdutoCatalogClient
{
    Task<ProdutoInfo?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
}
