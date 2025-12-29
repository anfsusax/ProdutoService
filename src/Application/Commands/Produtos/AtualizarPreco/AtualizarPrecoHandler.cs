using Application.Commands.Produtos.AtualizarPreco;
using Application.Common.Exceptions;
using Application.Interfaces;
using Microsoft.Extensions.Logging;

public sealed class AtualizarPrecoHandler
{
    private readonly IProdutoRepository _repository;
    private ILogger<AtualizarPrecoHandler> _logger;
    public AtualizarPrecoHandler(IProdutoRepository repository, ILogger<AtualizarPrecoHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task HandleAsync(AtualizarPrecoCommand command)
    {
        _logger.LogInformation("Iniciando o processamento do comando AtualizarPrecoCommand");

        var produto = await _repository.ObterPorIdAsync(command.ProdutoId);

        if (produto is null)
            throw new NotFoundException("Produto não encontrado.");

        produto.AtualizarPreco(command.Preco);

        await _repository.AtualizarAsync(produto);
        _logger.LogInformation("Comando AtualizarPrecoCommand processado com sucesso");
    }
}