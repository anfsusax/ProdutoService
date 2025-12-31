using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Produtos.Atualizar;

public class AtualizarProdutoHandler
{
    private readonly IProdutoRepository _repository;
    private readonly IOutboxWriter _outboxWriter;
    private readonly ILogger<AtualizarProdutoHandler> _logger;

    public AtualizarProdutoHandler(
        IProdutoRepository repository,
        IOutboxWriter outboxWriter,
        ILogger<AtualizarProdutoHandler> logger)
    {
        _repository = repository;
        _outboxWriter = outboxWriter;
        _logger = logger;
    }

    public async Task HandleAsync(AtualizarProdutoCommand command)
    {
        _logger.LogInformation("Iniciando o processamento do comando AtualizarProdutoCommand");

        var produto = await _repository.ObterPorIdAsync(command.Id);
        if (produto is null)
            throw new KeyNotFoundException("Produto não encontrado");

        produto.Atualizar(command.Nome, command.Descricao, command.Preco);

        await _repository.AtualizarAsync(produto);

        foreach (var domainEvent in produto.DomainEvents)
        {
            await _outboxWriter.AddAsync(domainEvent);
        }

        produto.ClearDomainEvents();

        _logger.LogInformation("Finalizando o processamento do comando AtualizarProdutoCommand");
    }
}
