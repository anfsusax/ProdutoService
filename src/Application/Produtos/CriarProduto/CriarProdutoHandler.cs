using Application.Interfaces;
using Domain.Entities;
 
namespace Application.Produtos.CriarProduto;

public class CriarProdutoHandler
{
    private readonly IProdutoRepository _repository;

    public CriarProdutoHandler(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CriarProdutoCommand command)
    {
        var produto = new Produto(
            command.Nome,
            command.Descricao,
            command.Preco
        );

        await _repository.AdicionarAsync(produto);
        return produto.Id;
    }
}
