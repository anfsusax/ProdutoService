using Domain.Entities;

namespace Application.Interfaces;

public interface IProdutoRepository
{
    Task AdicionarAsync(Produto produto);
    Task<Produto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Produto>> ObterTodosAsync();
}
