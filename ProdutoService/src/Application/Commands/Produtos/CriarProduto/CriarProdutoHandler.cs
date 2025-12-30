using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Produtos.CriarProduto;

public class CriarProdutoHandler
{
    private readonly IProdutoRepository _repository;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<CriarProdutoHandler> _logger;

    public CriarProdutoHandler(IProdutoRepository repository, IEventPublisher eventPublisher, ILogger<CriarProdutoHandler> logger)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task<Guid> HandleAsync(CriarProdutoCommand command)
    {
        _logger.LogInformation("Iniciando o processamento do comando CriarProdutoCommand");

        var produto = new Produto(
            command.Nome,
            command.Descricao,
            command.Preco
        );

        await _repository.AdicionarAsync(produto);

        foreach (var domainEvent in produto.DomainEvents)
        {
            await _eventPublisher.PublishAsync(domainEvent);
        }
         
        produto.ClearDomainEvents();

        _logger.LogInformation("Produto criado com sucesso. Id: {ProdutoId}", produto.Id);

        return produto.Id;
    }
}
