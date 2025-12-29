using Application.Commands.Produtos.AtualizarPreco;
using Application.Common.Exceptions;
using Application.Interfaces;

public sealed class AtualizarPrecoHandler
{
    private readonly IProdutoRepository _repository;

    public AtualizarPrecoHandler(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AtualizarPrecoCommand command)
    {
        var produto = await _repository.ObterPorIdAsync(command.ProdutoId);

        if (produto is null)
            throw new NotFoundException("Produto não encontrado.");

        produto.AtualizarPreco(command.Preco);

        await _repository.AtualizarAsync(produto);
    }
}