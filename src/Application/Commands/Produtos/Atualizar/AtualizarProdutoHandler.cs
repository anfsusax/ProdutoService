using Application.Common.Handlers;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Produtos.Atualizar;

public  class AtualizarProdutoHandler : BaseHandler<Produto>
{
    private readonly IProdutoRepository _repository;
    private ILogger<AtualizarProdutoHandler> _logger;
    public AtualizarProdutoHandler(IProdutoRepository repository, ILogger<AtualizarProdutoHandler> logger, IEventPublisher eventPublisher)
        : base(logger, eventPublisher)  
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task HandleAsync(AtualizarProdutoCommand command)
    {
        _logger.LogInformation("Iniciando o processamento do comando AtualizarProdutoCommand");

        var produto = await _repository.ObterPorIdAsync(command.Id);
        if (produto == null) throw new KeyNotFoundException("Produto não encontrado");

        await HandleDomainEventsAsync(produto, async () =>
        {
            produto.Atualizar(command.Nome, command.Descricao, command.Preco);
            await _repository.AtualizarAsync(produto);
        }, produto.DomainEvents);

        produto.ClearDomainEvents();
        _logger.LogInformation("Finalizando o processamento do comando AtualizarProdutoCommand");
    }
}
