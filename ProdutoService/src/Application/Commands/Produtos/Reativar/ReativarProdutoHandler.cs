using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Produtos.Reativar;

public class ReativarProdutoHandler
{
    private readonly IProdutoRepository _repository;
    private readonly IOutboxWriter _outboxWriter;
    private readonly ILogger<ReativarProdutoHandler> _logger;

    public ReativarProdutoHandler(
        IProdutoRepository repository,
        IOutboxWriter outboxWriter,
        ILogger<ReativarProdutoHandler> logger)
    {
        _repository = repository;
        _outboxWriter = outboxWriter;
        _logger = logger;
    }

    public async Task HandleAsync(ReativarProdutoCommand command)
    {
        _logger.LogInformation(
            "Iniciando o processamento do comando ReativarProdutoCommand para o ProdutoId: {ProdutoId}",
            command.ProdutoId);

        var produto = await _repository.ObterPorIdAsync(command.ProdutoId);
        if (produto is null)
        {
            _logger.LogWarning("Produto com Id: {ProdutoId} não encontrado.", command.ProdutoId);
            throw new KeyNotFoundException($"Produto com Id {command.ProdutoId} não encontrado.");
        }

        produto.Reativar();
        await _repository.AtualizarAsync(produto);

        foreach (var domainEvent in produto.DomainEvents)
        {
            await _outboxWriter.AddAsync(domainEvent);
        }

        produto.ClearDomainEvents();

        _logger.LogInformation(
            "Produto com Id: {ProdutoId} reativado com sucesso.",
            command.ProdutoId);
    }
}
