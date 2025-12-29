using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
 

namespace Produto.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ProdutoDbContext _context;

    public ProdutoRepository(ProdutoDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Domain.Entities.Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
    }
     
    public async Task<Domain.Entities.Produto?> ObterPorIdAsync(Guid id)
    {
        return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Domain.Entities.Produto>> ObterTodosAsync()
    {
        return await _context.Produtos
            .AsNoTracking()
            .ToListAsync();
    }

}
